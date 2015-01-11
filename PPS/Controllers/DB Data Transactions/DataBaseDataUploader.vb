' cDataBaseDataUploader.vb
'
' Manages upload (updates/ inserts) into Datas Tables
'
' 
' To do: 
'       - rst.open maybe highly time consumming on a big data table, add records separately? test needs to be done on big data 
'       - upload success message ? Progress bar ?   
'       - upload 1st period must be exactly month-01-year for monthly setup, year-01-01 for yearly setup -> to be documented
'       - 
'       - Upload N-1 
'   
'
'
' Known bugs:
'       - Update DB -> if srv.rst.open = error -> to be managed
'
'
'
' Author: Julien Monnereau
' Last modified: 11/12/2014


Imports System.Linq
Imports System.Data
Imports System.Collections.Generic
Imports System.Collections
Imports System.ComponentModel


Friend Class DataBaseDataUploader


#Region "Instance Variables"

    ' Objects
    Private PARENTOBJECT As Object
    Private srv As New ModelServer
    Friend PBar As PBarUI
    Private BCKGW As New BackgroundWorker

    ' Variables
    Private EntitiesNameKeyDictionary As Hashtable
    Private AccountsNameKeyDictionary As Hashtable
    Private AccountsKeyFormulaTypeDictionary As Hashtable
    Private VersionsNameTableAddressDictionary As Hashtable
    Private mDataSetDictionary As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double)))
    Private mVersionCode As String
    Friend uploadSuccessful As Boolean
    Friend mStartingPeriod As Integer

    ' Constants

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputEntitiesNameKeyDic As Hashtable, _
                   ByRef inputAccountsNameKeyDic As Hashtable, _
                   ByRef inputPARENTOBJECT As Object)

        PARENTOBJECT = inputPARENTOBJECT

        AccountsKeyFormulaTypeDictionary = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)
        EntitiesNameKeyDictionary = inputEntitiesNameKeyDic
        AccountsNameKeyDictionary = inputAccountsNameKeyDic

        VersionsNameTableAddressDictionary = VersionsMapping.GetVersionsHashTable(VERSIONS_NAME_VARIABLE, VERSIONS_CODE_VARIABLE)

        BCKGW.WorkerReportsProgress = True
        AddHandler BCKGW.DoWork, AddressOf BCKGW_DoWork
        AddHandler BCKGW.RunWorkerCompleted, AddressOf BCKGW_RunWorkerCompleted
        AddHandler BCKGW.ProgressChanged, AddressOf BCKGW_ProgressChanged

    End Sub

#End Region


#Region "Interface"


#Region "Update a DataSet Dictionary"

    ' Update the Data Base with the input array (from ModelDataSet)
    ' param1: dataSetDictionary (entities)(accounts)(periods)
    ' param2: version
    ' TOO HEAVY TO Take all data table RST...??!! find another way ??
    ' Upload 1st period must be exactly month-01-year for monthly setup, year-01-01 for yearly setup -> to be documented
    Friend Sub UpdateDataBase(ByRef dataSetDictionary As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double))), _
                              ByRef startingPeriod As Integer, _
                              ByRef versionCode As String)

        mDataSetDictionary = dataSetDictionary
        mStartingPeriod = startingPeriod
        mVersionCode = versionCode
        PBar = New PBarUI
        PBar.ProgressBarControl1.Launch(1, mDataSetDictionaryElementsCount())
        PBar.Show()
        BCKGW.RunWorkerAsync()

    End Sub

    Private Sub BCKGW_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs)

        Dim Fields(), Values() As Object
        Dim criteria, FormulaType, accountKey, entityKey As String

        Fields = {DATA_ASSET_ID_VARIABLE,
                  DATA_ACCOUNT_ID_VARIABLE, _
                  DATA_PERIOD_VARIABLE,
                  DATA_VALUE_VARIABLE}

        If srv.openRst(DATA_DATABASE + "." + mVersionCode, ModelServer.STATIC_CURSOR) = True Then                                                          ' Open data table recordset

            For Each entity In mDataSetDictionary.Keys
                entityKey = EntitiesNameKeyDictionary.Item(entity)

                For Each account In mDataSetDictionary(entity).Keys
                    accountKey = AccountsNameKeyDictionary.Item(account)
                    FormulaType = AccountsKeyFormulaTypeDictionary.Item(accountKey)

                    For Each period In mDataSetDictionary(entity)(account).Keys

                        If FormulaType = BALANCE_SHEET_ACCOUNT_FORMULA_TYPE _
                        AndAlso Not period = mStartingPeriod Then
                        Else
                            Values = {entityKey, _
                                      accountKey, _
                                      period, _
                                      mDataSetDictionary(entity)(account)(period)}

                            srv.rst.Filter = DATA_ASSET_ID_VARIABLE & "='" & Values(0) & "'" & _
                                             DATA_ACCOUNT_ID_VARIABLE & "='" & Values(1) & "'" & _
                                             DATA_PERIOD_VARIABLE & "='" & Values(2) & "'"

                            If srv.rst.EOF = True Or srv.rst.BOF = True Then
                                If Values(3) <> 0 Then srv.rst.AddNew(Fields, Values)
                            Else
                                If srv.rst.Fields(DATA_ARRAY_DATA_COLUMN).Value <> Values(3) Then
                                    srv.rst.Fields(DATA_ARRAY_DATA_COLUMN).Value = Values(3)
                                End If
                            End If
                        End If
                        BCKGW.ReportProgress(1)
                    Next
                Next
            Next
            Try
                srv.rst.UpdateBatch()
                uploadSuccessful = True
                ' -> Attention l'erreur ne viendra pas du updateBatch !!
            Catch ex As Exception
                uploadSuccessful = False
            End Try
            srv.rst.Close()
        Else
            uploadSuccessful = False
            ' catch error -> version data table could not be opened
        End If

    End Sub

    Private Sub BCKGW_ProgressChanged(sender As Object, e As ComponentModel.ProgressChangedEventArgs)

        PBar.ProgressBarControl1.AddProgress()

    End Sub

    Private Sub BCKGW_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)

        PARENTOBJECT.UpdateUploadResult(uploadSuccessful)

    End Sub

#End Region


#Region "Upload a single Value"

    Friend Function CheckAndUpdateSingleValueFromNames(ByVal entityName As String, _
                                                       ByVal accountName As String, _
                                                       ByVal period As Integer, _
                                                       ByVal value As Double, _
                                                       ByRef versionCode As String) As Boolean

        Dim entityKey As String = EntitiesNameKeyDictionary(entityName)
        Dim accountKey As String = AccountsNameKeyDictionary(accountName)
        Dim formulaType As String = AccountsKeyFormulaTypeDictionary(accountKey)

        If formulaType = BALANCE_SHEET_ACCOUNT_FORMULA_TYPE _
        AndAlso Not period = mStartingPeriod Then
            Return False
        Else
            Return UpdateSingleValue(entityKey, _
                                     accountKey, _
                                     period,
                                     value,
                                     versionCode)
        End If

    End Function

    Protected Friend Shared Function UpdateSingleValue(ByRef entityKey As String, _
                                      ByRef accountKey As String, _
                                      ByRef period As Integer, _
                                      ByRef value As Double, _
                                      ByRef versionCode As String) As Boolean

        Dim valueString As String = value.ToString("F", System.Globalization.CultureInfo.InvariantCulture)
        Dim srv As New ModelServer
        If srv.sqlQuery("REPLACE INTO " + DATA_DATABASE & "." & versionCode & _
                        "(" & DATA_ASSET_ID_VARIABLE & "," & _
                           DATA_ACCOUNT_ID_VARIABLE & "," & _
                           DATA_PERIOD_VARIABLE & "," & _
                           DATA_VALUE_VARIABLE & ")" & _
                        " VALUES ('" & entityKey & "','" & _
                                     accountKey & "'," & _
                                     period & "," & _
                                     valueString & ")") = True Then

            Return True
        Else
            Return False
        End If

    End Function

#End Region

#End Region


#Region "Utilities"

    Private Function mDataSetDictionaryElementsCount() As Int32

        Dim level1 As Int32 = mDataSetDictionary.Keys.Count
        If level1 > 0 Then
            Dim key1 As String = mDataSetDictionary.Keys(0)
            Dim level2 As Int32 = mDataSetDictionary(key1).Keys.Count
            If level2 > 0 Then
                Dim key2 As String = mDataSetDictionary(key1).Keys(0)
                Dim level3 As Int32 = mDataSetDictionary(key1)(key2).Count
                If level3 > 0 Then
                    Return level1 * level2 * level3
                Else
                    Return level1 * level2
                End If
            Else
                Return level1
            End If
        Else
            Return 0
        End If

    End Function


    ' Data Table Name (<-> version code) Finder
    ' Returns the version to be uploaded
    ' returns the table address or "" if the name of the version correspond to a version folder
    Private Function GetTableAddressFromName(ByRef inputVersionName As String) As String

        If inputVersionName <> "" Then
            If VersionsNameTableAddressDictionary(inputVersionName) <> "" Then
                Return VersionsNameTableAddressDictionary(inputVersionName)
            Else
                MsgBox("The selected version corresponds to a versions folder. A version name should be selected")
                Return ""
            End If
        Else
            If VersionsNameTableAddressDictionary(Version_Label.Caption) <> "" Then
                Return VersionsNameTableAddressDictionary(Version_Label.Caption)
            Else
                MsgBox("The selected version corresponds to a versions folder. A version name should be selected")
                Return ""
            End If
        End If

    End Function


#End Region




End Class

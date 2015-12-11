﻿Friend Class GlobalEnums

    Enum AccountsLookupOptions

        LOOKUP_OUTPUTS = 1
        LOOKUP_INPUTS
        LOOKUP_ALL
        LOOKUP_TITLES

    End Enum

    Enum DecompositionQueryType

        AXIS = 0
        FILTER

    End Enum

    Enum AnalysisAxis

        ENTITIES = 1
        CLIENTS
        PRODUCTS
        ADJUSTMENTS
        ACCOUNTS
        VERSIONS
        YEARS
        MONTHS
        YMONTHS
        WEEKS
        DAYS
        YDAYS

    End Enum

    Enum DataMapAxis

        FILTERS = 1
        VERSIONS
        ENTITIES
        ACCOUNTS
        PERIODS

    End Enum

    Enum VersionComparisonConfig

        Y_VERSIONS_COMPARISON = 1
        M_VERSIONS_COMPARISON
        Y_M_VERSIONS_COMPARISON

    End Enum

    Enum GlobalModels

        ACCOUNTS = 1
        ENTITYCURRENCY
        FILTERS
        FILTERSVALUES
        FACTSVERSIONS
        CURRENCIES
        RATESVERSIONS
        GLOBALFACT
        GLOBALFACTSDATA
        GLOBALFACTSVERSION
        USER
        GROUP
        GROUPALLOWEDENTITY
        FMODELINGACCOUNT
        AXIS_ELEM
        AXIS_FILTER
        ENTITYDISTRIBUTION

    End Enum

    Enum Process
        FINANCIAL = 0
        PDC
    End Enum


End Class

Enum CRUDAction
    CREATE = 1
    UPDATE = 2
    DELETE = 3
End Enum





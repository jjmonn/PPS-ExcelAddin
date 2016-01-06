Friend Class GlobalEnums

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
        USERALLOWEDENTITY
        FMODELINGACCOUNT
        AXIS_ELEM
        AXIS_FILTER
        ENTITYDISTRIBUTION
        AXISPARENT

    End Enum

 

End Class





Friend Class GlobalEnums

    Enum AccountsLookupOptions

        LOOKUP_OUTPUTS = 1
        LOOKUP_INPUTS
        LOOKUP_ALL
        LOOKUP_TITLES

    End Enum

    Enum FormulaTypes

        HARD_VALUE_INPUT = 1
        FORMULA
        AGGREGATION_OF_SUB_ACCOUNTS
        FIRST_PERIOD_INPUT
        TITLE

    End Enum

    Enum ConsolidationOptions

        AGGREGATION = 1
        RECOMPUTATION

    End Enum

    Enum ConversionOptions

        AVERAGE_PERIOD_RATE = 1
        END_OF_PERIOD_RATE

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

    End Enum


    Enum DataMapAxis

        FILTERS = 1
        VERSIONS
        ENTITIES
        ACCOUNTS
        YEARS
        MONTHS

    End Enum


End Class

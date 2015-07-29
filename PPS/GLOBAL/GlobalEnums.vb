﻿Friend Class GlobalEnums


    Enum ServerMessage

        SMSG_TEST_ANSWER = 0
        SMSG_AUTH_ANSWER
        SMSG_COMPUTE_RESULT

        SMSG_CREATE_ACCOUNT_ANSWER
        SMSG_READ_ACCOUNT_ANSWER
        SMSG_UPDATE_ACCOUNT_ANSWER
        SMSG_DELETE_ACCOUNT_ANSWER
        SMSG_LIST_ACCOUNT_ANSWER

        SMSG_CREATE_EXCHANGE_RATE_ANSWER
        SMSG_READ_EXCHANGE_RATE_ANSWER
        SMSG_UPDATE_EXCHANGE_RATE_ANSWER
        SMSG_DELETE_EXCHANGE_RATE_ANSWER
        SMSG_LIST_EXCHANGE_RATE_ANSWER

        SMSG_CREATE_RATE_VERSION_ANSWER
        SMSG_READ_RATE_VERSION_ANSWER
        SMSG_UPDATE_RATE_VERSION_ANSWER
        SMSG_DELETE_RATE_VERSION_ANSWER
        SMSG_LIST_RATE_VERSION_ANSWER

        SMSG_CREATE_FACT_ANSWER
        SMSG_READ_FACT_ANSWER
        SMSG_UPDATE_FACT_ANSWER
        SMSG_DELETE_FACT_ANSWER
        SMSG_LIST_FACT_ANSWER

        SMSG_CREATE_FILTER_ANSWER
        SMSG_READ_FILTER_ANSWER
        SMSG_UPDATE_FILTER_ANSWER
        SMSG_DELETE_FILTER_ANSWER
        SMSG_LIST_FILTER_ANSWER

        SMSG_CREATE_FILTERS_VALUE_ANSWER
        SMSG_READ_FILTERS_VALUE_ANSWER
        SMSG_UPDATE_FILTERS_VALUE_ANSWER
        SMSG_DELETE_FILTERS_VALUE_ANSWER
        SMSG_LIST_FILTER_VALUE_ANSWER

        SMSG_CREATE_VERSION_ANSWER
        SMSG_READ_VERSION_ANSWER
        SMSG_UPDATE_VERSION_ANSWER
        SMSG_DELETE_VERSION_ANSWER
        SMSG_LIST_VERSION_ANSWER

        SMSG_CREATE_PRODUCT_FILTER_ANSWER
        SMSG_READ_PRODUCT_FILTER_ANSWER
        SMSG_UPDATE_PRODUCT_FILTER_ANSWER
        SMSG_DELETE_PRODUCT_FILTER_ANSWER
        SMSG_LIST_PRODUCT_FILTER_ANSWER

        SMSG_CREATE_PRODUCT_ANSWER
        SMSG_READ_PRODUCT_ANSWER
        SMSG_UPDATE_PRODUCT_ANSWER
        SMSG_DELETE_PRODUCT_ANSWER
        SMSG_LIST_PRODUCT_ANSWER

        SMSG_CREATE_CLIENT_FILTER_ANSWER
        SMSG_READ_CLIENT_FILTER_ANSWER
        SMSG_UPDATE_CLIENT_FILTER_ANSWER
        SMSG_DELETE_CLIENT_FILTER_ANSWER
        SMSG_LIST_CLIENT_FILTER_ANSWER

        SMSG_CREATE_CLIENT_ANSWER
        SMSG_READ_CLIENT_ANSWER
        SMSG_UPDATE_CLIENT_ANSWER
        SMSG_DELETE_CLIENT_ANSWER
        SMSG_LIST_CLIENT_ANSWER

        SMSG_CREATE_ENTITY_FILTER_ANSWER
        SMSG_READ_ENTITY_FILTER_ANSWER
        SMSG_UPDATE_ENTITY_FILTER_ANSWER
        SMSG_DELETE_ENTITY_FILTER_ANSWER
        SMSG_LIST_ENTITY_FILTER_ANSWER

        SMSG_CREATE_ENTITY_ANSWER
        SMSG_READ_ENTITY_ANSWER
        SMSG_UPDATE_ENTITY_ANSWER
        SMSG_DELETE_ENTITY_ANSWER
        SMSG_LIST_ENTITY_ANSWER

        SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER
        SMSG_READ_ADJUSTMENT_FILTER_ANSWER
        SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER
        SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER
        SMSG_LIST_ADJUSTMENT_FILTER_ANSWER

        SMSG_CREATE_ADJUSTMENT_ANSWER
        SMSG_READ_ADJUSTMENT_ANSWER
        SMSG_UPDATE_ADJUSTMENT_ANSWER
        SMSG_DELETE_ADJUSTMENT_ANSWER
        SMSG_LIST_ADJUSTMENT_ANSWER

    End Enum

    Enum ClientMessage

        CMSG_TEST = 0

        CMSG_CREATE_ACCOUNT
        CMSG_READ_ACCOUNT
        CMSG_UPDATE_ACCOUNT
        CMSG_DELETE_ACCOUNT
        CMSG_LIST_ACCOUNT

        CMSG_CREATE_PRODUCTS_FILTER
        CMSG_READ_PRODUCTS_FILTER
        CMSG_UPDATE_PRODUCTS_FILTER
        CMSG_DELETE_PRODUCTS_FILTER
        CMSG_LIST_PRODUCTS_FILTER

        CMSG_CREATE_PRODUCT
        CMSG_READ_PRODUCT
        CMSG_UPDATE_PRODUCT
        CMSG_DELETE_PRODUCT
        CMSG_LIST_PRODUCT

        CMSG_CREATE_CLIENTS_FILTER
        CMSG_READ_CLIENTS_FILTER
        CMSG_UPDATE_CLIENTS_FILTER
        CMSG_DELETE_CLIENTS_FILTER
        CMSG_LIST_CLIENTS_FILTER

        CMSG_CREATE_CLIENT
        CMSG_READ_CLIENT
        CMSG_UPDATE_CLIENT
        CMSG_DELETE_CLIENT
        CMSG_LIST_CLIENT

        CMSG_CREATE_ADJUSTMENTS_FILTER
        CMSG_READ_ADJUSTMENTS_FILTER
        CMSG_UPDATE_ADJUSTMENTS_FILTER
        CMSG_DELETE_ADJUSTMENTS_FILTER
        CMSG_LIST_ADJUSTMENTS_FILTER

        CMSG_CREATE_ADJUSTMENT
        CMSG_READ_ADJUSTMENT
        CMSG_UPDATE_ADJUSTMENT
        CMSG_DELETE_ADJUSTMENT
        CMSG_LIST_ADJUSTMENT

        CMSG_CREATE_ENTITY_FILTER
        CMSG_READ_ENTITY_FILTER
        CMSG_UPDATE_ENTITY_FILTER
        CMSG_DELETE_ENTITY_FILTER
        CMSG_LIST_ENTITY_FILTER

        CMSG_CREATE_ENTITY
        CMSG_READ_ENTITY
        CMSG_UPDATE_ENTITY
        CMSG_DELETE_ENTITY
        CMSG_LIST_ENTITY

        CMSG_CREATE_EXCHANGE_RATE
        CMSG_READ_EXCHANGE_RATE
        CMSG_UPDATE_EXCHANGE_RATE
        CMSG_DELETE_EXCHANGE_RATE
        CMSG_LIST_EXCHANGE_RATE

        CMSG_CREATE_RATE_VERSION
        CMSG_READ_RATE_VERSION
        CMSG_UPDATE_RATE_VERSION
        CMSG_DELETE_RATE_VERSION
        CMSG_LIST_RATE_VERSION

        CMSG_CREATE_FACT
        CMSG_READ_FACT
        CMSG_UPDATE_FACT
        CMSG_DELETE_FACT
        CMSG_LIST_FACT

        CMSG_CREATE_FILTER
        CMSG_READ_FILTER
        CMSG_UPDATE_FILTER
        CMSG_DELETE_FILTER
        CMSG_LIST_FILTER

        CMSG_CREATE_FILTER_VALUE
        CMSG_READ_FILTER_VALUE
        CMSG_UPDATE_FILTER_VALUE
        CMSG_DELETE_FILTER_VALUE
        CMSG_LIST_FILTER_VALUE

        CMSG_CREATE_VERSION
        CMSG_READ_VERSION
        CMSG_UPDATE_VERSION
        CMSG_DELETE_VERSION
        CMSG_LIST_VERSION

        CMSG_AUTH_REQUEST
        CMSG_LOGOUT
        CMSG_COMPUTE_REQUEST
        MSG_MAX
    End Enum

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
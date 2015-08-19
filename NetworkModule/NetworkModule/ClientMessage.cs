﻿using System;

public enum ClientMessage : ushort
{
    CMSG_TEST = 0x000,
    CMSG_CREATE_ACCOUNT,
    CMSG_READ_ACCOUNT,
    CMSG_UPDATE_ACCOUNT,
    CMSG_DELETE_ACCOUNT,
    CMSG_LIST_ACCOUNT,
    CMSG_CREATE_PRODUCTS_FILTER,
    CMSG_READ_PRODUCTS_FILTER,
    CMSG_UPDATE_PRODUCTS_FILTER,
    CMSG_DELETE_PRODUCTS_FILTER,
    CMSG_LIST_PRODUCTS_FILTER,
    CMSG_CREATE_PRODUCT,
    CMSG_READ_PRODUCT,
    CMSG_UPDATE_PRODUCT,
    CMSG_DELETE_PRODUCT,
    CMSG_LIST_PRODUCT,
    CMSG_CREATE_CLIENTS_FILTER,
    CMSG_READ_CLIENTS_FILTER,
    CMSG_UPDATE_CLIENTS_FILTER,
    CMSG_DELETE_CLIENTS_FILTER,
    CMSG_LIST_CLIENTS_FILTER,
    CMSG_CREATE_CLIENT,
    CMSG_READ_CLIENT,
    CMSG_UPDATE_CLIENT,
    CMSG_DELETE_CLIENT,
    CMSG_LIST_CLIENT,
    CMSG_CREATE_ADJUSTMENTS_FILTER,
    CMSG_READ_ADJUSTMENTS_FILTER,
    CMSG_UPDATE_ADJUSTMENTS_FILTER,
    CMSG_DELETE_ADJUSTMENTS_FILTER,
    CMSG_LIST_ADJUSTMENTS_FILTER,
    CMSG_CREATE_ADJUSTMENT,
    CMSG_READ_ADJUSTMENT,
    CMSG_UPDATE_ADJUSTMENT,
    CMSG_DELETE_ADJUSTMENT,
    CMSG_LIST_ADJUSTMENT,
    CMSG_CREATE_ENTITY_FILTER,
    CMSG_READ_ENTITY_FILTER,
    CMSG_UPDATE_ENTITY_FILTER,
    CMSG_DELETE_ENTITY_FILTER,
    CMSG_LIST_ENTITY_FILTER,
    CMSG_CREATE_ENTITY,
    CMSG_READ_ENTITY,
    CMSG_UPDATE_ENTITY,
    CMSG_DELETE_ENTITY,
    CMSG_LIST_ENTITY,
    CMSG_CREATE_EXCHANGE_RATE,
    CMSG_READ_EXCHANGE_RATE,
    CMSG_UPDATE_EXCHANGE_RATE,
    CMSG_DELETE_EXCHANGE_RATE,
    CMSG_LIST_EXCHANGE_RATE,
    CMSG_CREATE_RATE_VERSION,
    CMSG_READ_RATE_VERSION,
    CMSG_UPDATE_RATE_VERSION,
    CMSG_DELETE_RATE_VERSION,
    CMSG_LIST_RATE_VERSION,
    CMSG_CREATE_FACT,
    CMSG_UPDATE_FACT,
    CMSG_DELETE_FACT,
    CMSG_CREATE_FILTER,
    CMSG_READ_FILTER,
    CMSG_UPDATE_FILTER,
    CMSG_DELETE_FILTER,
    CMSG_LIST_FILTER,
    CMSG_CREATE_FILTER_VALUE,
    CMSG_READ_FILTER_VALUE,
    CMSG_UPDATE_FILTER_VALUE,
    CMSG_DELETE_FILTER_VALUE,
    CMSG_LIST_FILTER_VALUE,
    CMSG_CREATE_VERSION,
    CMSG_READ_VERSION,
    CMSG_UPDATE_VERSION,
    CMSG_DELETE_VERSION,
    CMSG_LIST_VERSION,
    CMSG_CREATE_CURRENCY,
    CMSG_READ_CURRENCY,
    CMSG_UPDATE_CURRENCY,
    CMSG_DELETE_CURRENCY,
    CMSG_LIST_CURRENCY,
    CMSG_AUTH_REQUEST,
    CMSG_AUTHENTIFICATION,
    CMSG_COMPUTE_REQUEST,
    CMSG_CREATE_USER,
    CMSG_CREATE_GROUP,
    CMSG_SET_GROUP_RIGHTS,
    CMSG_ADD_GROUP_ENTITY,
    CMSG_DEL_GROUP_ENTITY,
    CMSG_LIST_GROUPS,
    CMSG_GET_GROUP_ENTITIES,
    CMSG_LIST_USERS,
    CMSG_SET_USER_GROUP,

    OpcodeMax

}

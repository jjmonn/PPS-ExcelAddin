﻿using System;

public enum ClientMessage : ushort
{
  CMSG_TEST = 0x000,
  CMSG_CREATE_ACCOUNT,
  CMSG_READ_ACCOUNT,
  CMSG_UPDATE_ACCOUNT,
  CMSG_DELETE_ACCOUNT,
  CMSG_LIST_ACCOUNT,
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
  CMSG_UPDATE_FACT_LIST,
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
  CMSG_UPDATE_GROUP,
  CMSG_ADD_GROUP_ENTITY,
  CMSG_DEL_GROUP_ENTITY,
  CMSG_LIST_GROUPS,
  CMSG_LIST_USERS,
  CMSG_UPDATE_USER,
  CMSG_SOURCED_COMPUTE,
  CMSG_COPY_VERSION,
  CMSG_GET_FACT_LOG,
  CMSG_GET_MAIN_CURRENCY,
  SMSG_SET_MAIN_CURRENCY,
  CMSG_CREATE_GLOBAL_FACT,
  CMSG_READ_GLOBAL_FACT,
  CMSG_UPDATE_GLOBAL_FACT,
  CMSG_DELETE_GLOBAL_FACT,
  CMSG_LIST_GLOBAL_FACT,
  CMSG_CREATE_GLOBAL_FACT_DATA,
  CMSG_READ_GLOBAL_FACT_DATA,
  CMSG_UPDATE_GLOBAL_FACT_DATA,
  CMSG_DELETE_GLOBAL_FACT_DATA,
  CMSG_LIST_GLOBAL_FACT_DATA,
  CMSG_CREATE_GLOBAL_FACT_VERSION,
  CMSG_READ_GLOBAL_FACT_VERSION,
  CMSG_UPDATE_GLOBAL_FACT_VERSION,
  CMSG_DELETE_GLOBAL_FACT_VERSION,
  CMSG_LIST_GLOBAL_FACT_VERSION,
  CMSG_CRUD_ACCOUNT_LIST,
  CMSG_LIST_GROUP_ENTITIES,
  CMSG_READ_USER,
  CMSG_DELETE_USER,
  CMSG_READ_GROUP,
  CMSG_DELETE_GROUP,
  CMSG_CRUD_FILTER_LIST,
  CMSG_CRUD_CURRENCY_LIST,
  CMSG_CRUD_EXCHANGE_RATE_LIST,
  CMSG_CRUD_RATE_VERSION_LIST,
  CMSG_CRUD_VERSION_LIST,
  CMSG_CRUD_GLOBAL_FACT_LIST,
  CMSG_CRUD_GLOBAL_FACT_DATA_LIST,
  CMSG_CRUD_GLOBAL_FACT_VERSION_LIST,
  CMSG_CRUD_FILTER_VALUE_LIST,
  CMSG_CREATE_FMODELING_ACCOUNT,
  CMSG_READ_FMODELING_ACCOUNT,
  CMSG_UPDATE_FMODELING_ACCOUNT,
  CMSG_DELETE_FMODELING_ACCOUNT,
  CMSG_LIST_FMODELING_ACCOUNT,
  CMSG_CRUD_FMODELING_ACCOUNT_LIST,
  CMSG_CREATE_AXIS_FILTER,
  CMSG_READ_AXIS_FILTER,
  CMSG_UPDATE_AXIS_FILTER,
  CMSG_DELETE_AXIS_FILTER,
  CMSG_LIST_AXIS_FILTER,
  CMSG_CRUD_AXIS_FILTER_LIST,
  CMSG_CREATE_AXIS_ELEM,
  CMSG_READ_AXIS_ELEM,
  CMSG_UPDATE_AXIS_ELEM,
  CMSG_DELETE_AXIS_ELEM,
  CMSG_LIST_AXIS_ELEM,
  CMSG_CRUD_AXIS_ELEM_LIST,
  CMSG_READ_ENTITY_CURRENCY,
  CMSG_UPDATE_ENTITY_CURRENCY,
  CMSG_LIST_ENTITY_CURRENCY,
  CMSG_CRUD_ENTITY_CURRENCY,
  CMSG_READ_ENTITY_DISTRIBUTION,
  CMSG_UPDATE_ENTITY_DISTRIBUTION,
  CMSG_LIST_ENTITY_DISTRIBUTION,
  CMSG_CRUD_ENTITY_DISTRIBUTION,
  OpcodeMax
}

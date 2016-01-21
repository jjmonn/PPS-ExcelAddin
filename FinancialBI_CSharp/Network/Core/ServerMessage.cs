﻿using System;

public enum ServerMessage : int
{
  SMSG_TEST_ANSWER = 0x000,

  SMSG_CREATE_ACCOUNT_ANSWER,
  SMSG_READ_ACCOUNT_ANSWER,
  SMSG_UPDATE_ACCOUNT_ANSWER,
  SMSG_DELETE_ACCOUNT_ANSWER,
  SMSG_LIST_ACCOUNT_ANSWER,

  SMSG_CREATE_EXCHANGE_RATE_ANSWER,
  SMSG_READ_EXCHANGE_RATE_ANSWER,
  SMSG_UPDATE_EXCHANGE_RATE_ANSWER,
  SMSG_DELETE_EXCHANGE_RATE_ANSWER,
  SMSG_LIST_EXCHANGE_RATE_ANSWER,

  SMSG_CREATE_RATE_VERSION_ANSWER,
  SMSG_READ_RATE_VERSION_ANSWER,
  SMSG_UPDATE_RATE_VERSION_ANSWER,
  SMSG_DELETE_RATE_VERSION_ANSWER,
  SMSG_LIST_RATE_VERSION_ANSWER,

  SMSG_UPDATE_FACT_LIST_ANSWER,
  SMSG_READ_FACT_ANSWER,
  SMSG_UPDATE_FACT_ANSWER,
  SMSG_DELETE_FACT_ANSWER,
  SMSG_LIST_FACT_ANSWER,

  SMSG_CREATE_FILTER_ANSWER,
  SMSG_READ_FILTER_ANSWER,
  SMSG_UPDATE_FILTER_ANSWER,
  SMSG_DELETE_FILTER_ANSWER,
  SMSG_LIST_FILTER_ANSWER,

  SMSG_CREATE_FILTERS_VALUE_ANSWER,
  SMSG_READ_FILTERS_VALUE_ANSWER,
  SMSG_UPDATE_FILTERS_VALUE_ANSWER,
  SMSG_DELETE_FILTERS_VALUE_ANSWER,
  SMSG_LIST_FILTER_VALUE_ANSWER,

  SMSG_CREATE_VERSION_ANSWER,
  SMSG_READ_VERSION_ANSWER,
  SMSG_UPDATE_VERSION_ANSWER,
  SMSG_DELETE_VERSION_ANSWER,
  SMSG_LIST_VERSION_ANSWER,

  SMSG_CREATE_CURRENCY_ANSWER,
  SMSG_READ_CURRENCY_ANSWER,
  SMSG_UPDATE_CURRENCY_ANSWER,
  SMSG_DELETE_CURRENCY_ANSWER,
  SMSG_LIST_CURRENCY_ANSWER,

  SMSG_AUTH_REQUEST_ANSWER,
  SMSG_AUTH_ANSWER,
  SMSG_COMPUTE_RESULT,
  SMSG_CREATE_USER_ANSWER,
  SMSG_CREATE_GROUP_ANSWER,
  SMSG_UPDATE_GROUP_ANSWER,
  SMSG_ADD_USER_ENTITY_ANSWER,
  SMSG_DEL_USER_ENTITY_ANSWER,
  SMSG_LIST_GROUPS,
  SMSG_LIST_USERS,
  SMSG_UPDATE_USER_ANSWER,
  SMSG_SOURCED_COMPUTE_RESULT,
  SMSG_COPY_VERSION_ANSWER,
  SMSG_GET_FACT_LOG_ANSWER,
  SMSG_GET_MAIN_CURRENCY_ANSWER,
  SMSG_SET_MAIN_CURRENCY_ANSWER,

  SMSG_CREATE_GLOBAL_FACT_ANSWER,
  SMSG_READ_GLOBAL_FACT_ANSWER,
  SMSG_UPDATE_GLOBAL_FACT_ANSWER,
  SMSG_DELETE_GLOBAL_FACT_ANSWER,
  SMSG_LIST_GLOBAL_FACT_ANSWER,

  SMSG_CREATE_GLOBAL_FACT_DATA_ANSWER,
  SMSG_READ_GLOBAL_FACT_DATA_ANSWER,
  SMSG_UPDATE_GLOBAL_FACT_DATA_ANSWER,
  SMSG_DELETE_GLOBAL_FACT_DATA_ANSWER,
  SMSG_LIST_GLOBAL_FACT_DATA_ANSWER,

  SMSG_CREATE_GLOBAL_FACT_VERSION_ANSWER,
  SMSG_READ_GLOBAL_FACT_VERSION_ANSWER,
  SMSG_UPDATE_GLOBAL_FACT_VERSION_ANSWER,
  SMSG_DELETE_GLOBAL_FACT_VERSION_ANSWER,
  SMSG_LIST_GLOBAL_FACT_VERSION_ANSWER,

  SMSG_CRUD_ACCOUNT_LIST_ANSWER,

  SMSG_COMPUTE_REQUEST,
  SMSG_SOURCED_COMPUTE_REQUEST,
  SMSG_MINION_AUTHENTICATE_ANSWER,

  SMSG_LIST_USER_ENTITIES_ANSWER,
  SMSG_READ_USER_ANSWER,
  SMSG_DELETE_USER_ANSWER,
  SMSG_READ_GROUP_ANSWER,
  SMSG_DELETE_GROUP_ANSWER,

  SMSG_CRUD_FILTER_LIST_ANSWER,
  SMSG_CRUD_CURRENCY_LIST_ANSWER,
  SMSG_CRUD_EXCHANGE_RATE_LIST_ANSWER,
  SMSG_CRUD_RATE_VERSION_LIST_ANSWER,
  SMSG_CRUD_VERSION_LIST_ANSWER,
  SMSG_CRUD_GLOBAL_FACT_LIST_ANSWER,
  SMSG_CRUD_GLOBAL_FACT_DATA_LIST_ANSWER,
  SMSG_CRUD_GLOBAL_FACT_VERSION_LIST_ANSWER,
  SMSG_CRUD_FILTER_VALUE_LIST_ANSWER,
  SMSG_READ_USER_ENTITY,

  SMSG_IS_VALID_ACCOUNT,

  SMSG_CREATE_FMODELING_ACCOUNT_ANSWER,
  SMSG_READ_FMODELING_ACCOUNT_ANSWER,
  SMSG_UPDATE_FMODELING_ACCOUNT_ANSWER,
  SMSG_DELETE_FMODELING_ACCOUNT_ANSWER,
  SMSG_LIST_FMODELING_ACCOUNT_ANSWER,
  SMSG_CRUD_FMODELING_ACCOUNT_LIST_ANSWER,

  SMSG_CREATE_AXIS_FILTER_ANSWER,
  SMSG_READ_AXIS_FILTER_ANSWER,
  SMSG_UPDATE_AXIS_FILTER_ANSWER,
  SMSG_DELETE_AXIS_FILTER_ANSWER,
  SMSG_LIST_AXIS_FILTER_ANSWER,
  SMSG_CRUD_AXIS_FILTER_LIST_ANSWER,

  SMSG_CREATE_AXIS_ELEM_ANSWER,
  SMSG_READ_AXIS_ELEM_ANSWER,
  SMSG_UPDATE_AXIS_ELEM_ANSWER,
  SMSG_DELETE_AXIS_ELEM_ANSWER,
  SMSG_LIST_AXIS_ELEM_ANSWER,
  SMSG_CRUD_AXIS_ELEM_LIST_ANSWER,

  SMSG_READ_ENTITY_CURRENCY_ANSWER,
  SMSG_UPDATE_ENTITY_CURRENCY_ANSWER,
  SMSG_DELETE_ENTITY_CURRENCY_ANSWER,
  SMSG_LIST_ENTITY_CURRENCY_ANSWER,
  SMSG_CRUD_ENTITY_CURRENCY_LIST_ANSWER,

  SMSG_READ_ENTITY_DISTRIBUTION_ANSWER,
  SMSG_UPDATE_ENTITY_DISTRIBUTION_ANSWER,
  SMSG_DELETE_ENTITY_DISTRIBUTION_ANSWER,
  SMSG_LIST_ENTITY_DISTRIBUTION_ANSWER,
  SMSG_CRUD_ENTITY_DISTRIBUTION_LIST_ANSWER,

  SMSG_FACT_UPDATED,

  SMSG_CREATE_ENTITY_DISTRIBUTION_ANSWER,

  SMSG_CREATE_COMMIT_ANSWER,
  SMSG_READ_COMMIT_ANSWER,
  SMSG_UPDATE_COMMIT_ANSWER,
  SMSG_DELETE_COMMIT_ANSWER,
  SMSG_LIST_COMMIT_ANSWER,
  SMSG_CRUD_COMMIT_LIST_ANSWER,

  SMSG_GET_FACT_ANSWER,

  SMSG_CREATE_AXIS_OWNER_ANSWER,
  SMSG_READ_AXIS_OWNER_ANSWER,
  SMSG_UPDATE_AXIS_OWNER_ANSWER,
  SMSG_DELETE_AXIS_OWNER_ANSWER,
  SMSG_LIST_AXIS_OWNER_ANSWER,
  SMSG_CRUD_AXIS_OWNER_LIST_ANSWER,
  SMSG_GET_COMMIT_LOG_ANSWER,
  SMSG_GET_AXIS_ELEM_LOG_ANSWER,
  OpcodeMax
}
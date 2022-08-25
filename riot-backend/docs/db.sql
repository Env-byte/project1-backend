create schema if not exists test;

SET search_path TO test, extensions;
/**SET search_path TO test, extensions;**/
CREATE TABLE if not exists users
(
    id         SERIAL PRIMARY KEY,
    first_name VARCHAR(100)            NOT NULL,
    last_name  VARCHAR(100)            NOT NULL,
    email      extensions.citext       NOT NULL,
    type       extensions.login_method NOT NULL,
    token      text
);
CREATE INDEX ON users ((lower(email)));

CREATE TABLE if not exists teams
(
    id         SERIAL PRIMARY KEY,
    name       VARCHAR(100) NOT NULL,
    tft_set    smallint     NOT NULL,
    created_by integer REFERENCES users (id),
    create_on  timestamp
);

CREATE TABLE if not exists team_champions
(
    championId varchar(50) PRIMARY KEY,
    team_id    integer REFERENCES teams (id),
    item_ids   smallint[]
);

CREATE TABLE if not exists summoners
(
    id              varchar(65)  not null,
    account_id      varchar(56)  not null,
    puuid           varchar(78)  not null,
    name            varchar(100) not null,
    profile_icon_id int          not null,
    revision_date   bigint       not null,
    summoner_level  bigint       not null,
    last_update     timestamp default now()
);

CREATE TABLE if not exists match
(
    id   varchar(150),
    data json
);
/**
  store the last 20 matches for the summoner here. 
  once a row from the matches table has 0 references in this table delete that row.
 */
CREATE table if not exists summoner_matches
(
    summoner_id varchar(65) not null,
    match_id    varchar(150)

)
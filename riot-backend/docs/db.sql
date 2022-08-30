create schema if not exists public;

SET search_path TO public, extensions;
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
    championId varchar(50) not null,
    team_id    integer REFERENCES teams (id),
    item_ids   smallint[]
);
create index on team_champions (team_id);

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
create unique index on summoners (puuid);


CREATE TABLE if not exists match
(
    puuid varchar(78),
    data  text
);
create unique index on match (puuid);

/**
  store the last 20 matches for the summoner here. 
  once a row from the matches table has 0 references in this table delete that row.
 */
CREATE table if not exists summoner_matches
(
    summoner_puuid varchar(78) not null,
    match_puuid    varchar(78)
);
create unique index on summoner_matches (summoner_puuid, match_puuid);

CREATE TABLE if not exists summoner_league
(
    summoner_id varchar(78),
    data        text
);
create unique index on summoner_league (summoner_id);
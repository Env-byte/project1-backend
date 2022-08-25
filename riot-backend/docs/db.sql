
create schema if not exists test;

SET search_path TO test, extensions;
/**SET search_path TO test, extensions;**/
CREATE TABLE if not exists users
(
    id         SERIAL PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name  VARCHAR(100) NOT NULL,
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
CREATE EXTENSION citext;

CREATE SCHEMA general;

CREATE TYPE login_method AS ENUM ('google');

CREATE TABLE general.users
(
    id         SERIAL PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name  VARCHAR(100) NOT NULL,
    email      citext       NOT NULL,
    type       login_method NOT NULL,
    token      text
);
CREATE UNIQUE INDEX ON general.users ((lower(email)));

CREATE SCHEMA tft;

CREATE TABLE tft.teams
(
    id         SERIAL PRIMARY KEY,
    name       VARCHAR(100) NOT NULL,
    tft_set    smallint     NOT NULL,
    created_by integer REFERENCES general.users (id),
    create_on  timestamp
);

CREATE TABLE tft.team_champions
(
    championId varchar(50) PRIMARY KEY,
    team_id    integer REFERENCES tft.teams (id),
    item_ids   smallint[]
);
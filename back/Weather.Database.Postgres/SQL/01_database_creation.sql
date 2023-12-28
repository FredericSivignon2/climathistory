-- Création de la table Country
CREATE TABLE Country (
    CountryId BIGSERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

-- Création de la table Location
CREATE TABLE Location (
    LocationId BIGSERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    CountryId BIGINT NOT NULL,
    FOREIGN KEY (CountryId) REFERENCES Country(CountryId)
);

-- Création de la table Temperatures
CREATE TABLE Temperatures (
    LocationId BIGINT NOT NULL,
    Date DATE NOT NULL,
    MinTemperature DECIMAL,
    MaxTemperature DECIMAL,
    AvgTemperature DECIMAL,
    PRIMARY KEY (LocationId, Date),
    FOREIGN KEY (LocationId) REFERENCES Location(LocationId)
);
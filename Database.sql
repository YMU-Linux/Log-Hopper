DROP DATABASE LogHopper
CREATE DATABASE LogHopper;

USE LogHopper;


CREATE TABLE Users (
    UserID INT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    RolePower VARCHAR(50)
);

CREATE TABLE Tokens (
    TokenID INT PRIMARY KEY,
    Token VARCHAR(255) NOT NULL,
    TimeToLive INT
);

CREATE TABLE Settings (
    SettingsID INT PRIMARY KEY,
    Settings VARCHAR(255) NOT NULL
);

CREATE TABLE FilterStorage (
    FilterID INT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Tags VARCHAR(255),
    Filter TEXT
);

CREATE TABLE Users_Token (
    UserID INT,
    TokenID INT,
    PRIMARY KEY (UserID, TokenID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (TokenID) REFERENCES Tokens(TokenID) ON DELETE CASCADE
);

CREATE TABLE User_Settings (
    UserID INT,
    SettingsID INT,
    PRIMARY KEY (UserID, SettingsID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (SettingsID) REFERENCES Settings(SettingsID) ON DELETE CASCADE
);

CREATE TABLE User_Filter (
    UserID INT,
    FilterID INT,
    PRIMARY KEY (UserID, FilterID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (FilterID) REFERENCES FilterStorage(FilterID) ON DELETE CASCADE
);

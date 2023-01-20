CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

DROP TABLE accounts;
DROP TABLE favorites;
DROP TABLE recipes;
DROP TABLE ingredients;

CREATE TABLE recipes(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(255),
  instructions VARCHAR(255),
  imgUrl VARCHAR(255),
  category VARCHAR(100),
  creatorId VARCHAR(255) NOT NULL,

  Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO recipes
(title,instructions,`imgUrl`,category,`creatorId`)
VALUES
('avocado toast', 'Crisp the bread, spread avocado, top with seasoning', 'https://images.unsplash.com/photo-1588137378633-dea1336ce1e2?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NHx8YXZvY2FkbyUyMHRvYXN0fGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60', 'Breakfast', '6387da8cdf89658f5215b5b5');

CREATE TABLE ingredients(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  name VARCHAR(255),
  quantiry VARCHAR(255),
  recipeId INT NOT NULL,

  Foreign Key (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE favorites(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,

  Foreign Key (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  Foreign Key (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
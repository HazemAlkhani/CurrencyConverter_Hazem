CREATE TABLE Conversions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    `date` DATE NOT NULL,
    source VARCHAR(3) NOT NULL,
    target VARCHAR(3) NOT NULL,
    value INT NOT NULL,
    result DECIMAL(10, 2) NOT NULL
);

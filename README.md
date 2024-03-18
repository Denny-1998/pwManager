# pwManager

## Short description
This project will consist of a password manager running locally. 

The program will be written in C# using some functions of the built in libraries for cryptography; mainly consisting of the "System.Security.Cryptography" package. 
For now the password vault will be stored in a locally running Microsoft SQL server. 

The encryption used for storing the data in the database will be symmetric using a hash of the master password and username. 
Since only one user will access the database and it will not be shared with anyone, a Public/private key model is not needed. 
This way the user will use the same key (username + password) to both encrypt and decrypt the data. This will be done using the built in tools for AES + CBC (Cipher Block Chaining).
For hashing a SHA alrogrithm is used. Most probably SHA512. 

Since this project is running locally, no thought was given to authentication against a potential server. 
This would be a requirement if the project had a cloud database and with that an api taking the workload of authenticating users and sending out the correct password vault for the given user. 


## Diagrams
### A general overview of the projects architecture and the used algorithms 

![image](https://github.com/Denny-1998/pwManager/assets/89900734/77a6a937-be69-4ad4-862d-7a84e2e51358)

### The database table 

![image](https://github.com/Denny-1998/pwManager/assets/89900734/2b303088-c358-465e-aa17-ae45ab9beb29)


# Project description

## Overview
pwManager is a password manager application developed in C# using Entity Framework for database interaction. The application enables users to securely store and manage their account credentials on a single device. Key features include user authentication, account creation, password encryption, and manipulation of stored accounts.

## Security Measures
### Password Encryption
- Passwords are encrypted using Advanced Encryption Standard (AES) encryption before being stored in the database.
- The EncryptionHelper class provides methods for encrypting and decrypting sensitive data using AES encryption algorithms. This ensures that passwords are securely stored and protected from unauthorized access.
- A unique initialization vector (IV) for each stored account is used to create another layer of security.  

### User Authentication
- User authentication is performed securely through hashed passwords and salting techniques to enhance security.
- The HashHelper class offers methods for hashing passwords using SHA256 and SHA512 algorithms, providing a one-way transformation of passwords into irreversible hashes.
- Salting is incorporated into the hashing process to add an extra layer of security by appending a unique salt value to each password before hashing. This prevents attackers from using precomputed hash tables (rainbow tables) to crack passwords.
- Iterative hashing is employed to increase the computational cost of hashing, making it more difficult for attackers to perform brute-force attacks or dictionary attacks on passwords.
- Even if the username is wrong, a hash is calculated with the same number of iterations as for any authenticated user to make it harder to guess for an intruder, if the username or password was wrong. 

### Database Integration
- Entity Framework is utilized to interact with the SQL Server database, ensuring secure communication and data access.
- The Database class acts as an intermediary between the application and the database context, enforcing data integrity and access controls.

## User Interface
The user interface comprises three main windows:
1. **LogInWindow:** Provides a login interface for users to authenticate and access the main application functionalities. ![image](https://github.com/Denny-1998/pwManager/assets/89900734/0b87a307-8971-4a51-939d-d414e06b2e18)

2. **MainWindow:** Displays a list of decrypted accounts and allows users to add, edit, or delete accounts. ![image](https://github.com/Denny-1998/pwManager/assets/89900734/f516a3b8-00cf-43ff-acc9-88612f101245)

3. **AddAccount:** Offers a dialog for adding new accounts with customizable name, username, and password fields. ![image](https://github.com/Denny-1998/pwManager/assets/89900734/a8fc1631-6ed0-4ce6-b155-ae703c3cdb3b)


## Functionality
- **Account Management:** Users can add, edit, delete, and view account details, including name, username, and decrypted passwords.
- **Password Generation:** The application allows users to generate random passwords using a set of random characters including letters, numbers and special characters.

## Future Enhancements
- **User Experience Improvements:** Enhance the user interface design for improved usability and aesthetics.
- **Error Handling:** Implement comprehensive error handling mechanisms to gracefully manage exceptions and provide informative feedback to users.

## Conclusion
In conclusion, pwManager is a comprehensive password management tool designed to securely store and manage user credentials. By leveraging AES encryption, hashed passwords, and salting techniques, it ensures robust security measures are in place to protect sensitive data. Features like random password generation enhance the overall security of the project. Future enhancements may focus on improving UI design, error handling and perhaps an api to use this on multiple devices; this however, would need a few thoughts on securing the data in transport. 

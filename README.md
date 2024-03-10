# pwManager

## Short description
This project will consist of a password manager running locally. 

The program will be written in C# using some functions of the built in libraries for cryptography; mainly consisting of the "System.Security.Cryptography" package. 
For now the password vault will be stored in a locally running Microsoft SQL server. 

The encryption used for storing the data in the database will be symmetric using a hash of the master password and username. 
Since only one user will access the database and it will not be shared with anyone, a Public/private key model is not needed. 
This way the user will use the same key (username + password) to both encrypt and decrypt the data. 
For hashing a SHA alrogrithm is used. Most probably SHA512. 

Since this project is running locally, no thought was given to authentication against a potential server. 
This would be a requirement if the project had a cloud database and with that an api taking the workload of authenticating users and sending out the correct password vault for the given user. 


## Diagrams
A general overview of the projects architecture and the used algorithms 
![image](https://github.com/Denny-1998/pwManager/assets/89900734/77a6a937-be69-4ad4-862d-7a84e2e51358)

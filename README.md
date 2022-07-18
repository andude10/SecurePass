# SecurePass

Password manager. On login, if the master-password is correct, the SQLite database is decrypted. On exit,
the database is encrypted back.

## The application has the following features:

- Application saves all password changes
- Customizable password generator
- Protected notes
- The password database can be decrypted knowing the master-password through other programs.

## A bit about the implementation:

Application used the repository pattern. There was a problem with database encryption because standard SQLite doesn't support encryption, so I used SQLCipher which can be encrypted.

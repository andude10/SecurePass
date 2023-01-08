Password manager. On login, if the master-password is correct, the SQLite database is decrypted. On exit,
the database is encrypted back.

## Features:

- Password history
- Customizable password generator
- Passwords tags
- Encrypted password notes
- The general format of the SQLite database, the database can be decrypted in other ways by knowing the master password

## Implementation

This is a freelance project and the order required the use of SQLite. But the normal version of the database does not support encryption, for this reason I decided to use SQLCipher.

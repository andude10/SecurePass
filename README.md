# SecurePass

Password manager application, on login, if the master-password is correct, the SQLite database is decrypted. On exit, the database is encrypted back.

The application has the following features:

-Application saves all password changes
-Customizable password generator
-Protected notes
-Creating the SQLCipher base, knowing the master-password, you can decrypt the base through other programs

A bit about implementation:
I used the MVVM pattern using the Microsoft.Toolkit.Mvvm library. All VMs do not interact with the database directly; business logic is used for this.
There was a problem with encrypting the database because standard SQLite does not support encryption. So I used SQLCipher, which can be encrypted.

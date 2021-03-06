# Database Creation Tools

This document describes how the SQLite database creation tools work. These tools help desktop software code generate a new database from DDL statements and then populate it with lookup information. Typically it is used when a user wants to start a new *blank* user database.

## Database Structure File (DDL)

A single data definition language (DDL) file contains the commands to create all the tables and views contained within a database. Typically this file is called `<toolname>_structure.sql` but the name can be anything you want.

This file is generated by running a series of SQLite shell commands. For convenience these commands are stored in a single text file called `workbench_export_commands.txt` and then these commands executed with a single operations within a DOS batch file called `workbench_export.bat`. So the command looks like:

```
<absolute_path>\sqlite3.exe <absolute_path>\workbench.db < workbench_export_commands.txt
```

By storing this single command in a batch file, it becomes easy to export a SQLite database simply by double clicking this batch file in Windows Explorer.

## SQLite Export Commands

To export an entire database including both the schema and the data run the following two commands **from within the SQLite command shell**. The first command sets the SQLite property where the output will go. The `.dump` command then performs the export, sending the output to the specified file. Note that this operation produces a file that includes a **transaction** and also helpfully disables referential integrity at the top of the file and turns it back on at the bottom.

```
.output ./output_file.sql
.dump
```

To export just the schema for a database type the following commands. Note that this produces a file that does **not** include a transaction or referential integrity disabling.

```
.output ./output_file.sql
.schema
```

Finally, to export the data for a particular table you run the following commands. First you tell SQLite that the mode for subsequent command should be *insert*. Then you select the rows from the desired table. What is produced is actually a series of `INSERT` statements. 

```
.mode insert
.output ./my_table.sql
SELECT * FROM MyTable;
```

What's annoying about this operation is that all the SQL statements have the word `table` in the output where the database table would be. So they look like the following. This can be handled with a regex at the time that you want to use the commands.

```
INSERT INTO table VALUES(1,1,'Model Run',0);
```

## Gluing It All Together



# Creating a Database Update

Follow these steps to generate a new software update.

1. Identify the last used database version number. This will be the **base** version number.
2. Generate a clean version of the **base** database:
   1. Open a DOS prompt
   2. Run the SQLite command line software.
   3. Create an empty database with the command `.open base.db`
   4. Generate the database structure with the command `.read latest.sql`
3. Increment  the base version number by one to identify the **latest** version number.
4. Identify the database that contains all the **latest** features.
5. Confirm that the `VersionInfo` table in the **latest** database contains the latest version number and also todays date.
6. Edit the batch file called `workbench_export.bat` and ensure that the database path correctly points to the latest database. 
7. Run the `workbench_export.bat` file to generate a new file called `latest.sql`. This file now contains the DDL to generate a clean version of the latest database.
8. Generate a new, clean copy of the latest database. Note that this database is subtly different than you're actual latest database. This *clean* copy should only contain the essential structural elements and lookup data, whereas your working copy might have CHaMP data and metrics in it.
   1. Open a DOS prompt
   2. Run the SQLite command line software.
   3. Create an empty database with the command `.open latest.db`
   4. Generate the database structure with the command `.read latest.sql`
9. Generate the update DDL that will upgrade databases from the previous, base version to the latest version. Run the command `workbench_export_update.bat` at the DOS prompt.
10. Rename the SQL script that's generated `update_XXX.sql` where XXX **must** be the 3 digit, zero padded, latest version number.
11. Open and review the update script. Eyeball every command, but pay special attention to:
    1. The last SQL command should be an UPDATE statement to `VersionInfo` that sets the database version to the latest version number and date.
12. Delete the `base.db` and `latest.db` database files. They aren't needed any more and shouldn't go in git.

## Resources

* [SQLite export features using .dump](http://www.sqlitetutorial.net/sqlite-dump/)
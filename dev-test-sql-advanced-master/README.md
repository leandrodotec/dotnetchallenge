# dev-test-sql-advanced
An advanced level question for developers

There is a table with a unique id (id) as an auto-incrementing serial, a unique device identifier that has multiple rows against it (devid), and some data for those devices (data); 1 row for each piece of data:

`CREATE TABLE IF NOT EXISTS t1
    (id integer, devid integer, data float)`
    
Someone has made a mistake and instead of 60 rows per device, there ended up being up to 200 rows per device.

**Write some SQL to keep only the 60 most recent rows for each device.**

The sample data only has 500 rows, but the query you write cannot assume that there are a small number of rows, or that the id's of the rows to delete are known beforehand.

http://rextester.com/l/postgresql_online_compiler is a useful tool to attempt to perform this test online, but you do not need to use it.  You only need to submit the answer query.

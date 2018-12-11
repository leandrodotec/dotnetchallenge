/*
**Write some SQL to keep only the 60 most recent rows for each device.**

*/

/*
This query selects the 60 most recent rows for each device
*/
SELECT * FROM (
			SELECT
				 id,
				 devid,
				 data,
				 ROW_NUMBER () OVER (
					 PARTITION BY devid
					 ORDER BY id desc
				 ) 
			FROM t1
			) rows_to_keep
WHERE 
	row_number <=60


/*
This query deletes all rows that should not be kept
*/

DELETE FROM t1
WHERE 
	t1.id NOT IN 
			(
				SELECT id FROM (
							SELECT
								 id,
								 ROW_NUMBER () OVER (
									 PARTITION BY devid
									 ORDER BY id desc
								 ) 
							FROM t1
							) rows_to_keep
				WHERE 
					row_number <=60);

SELECT * FROM t1;
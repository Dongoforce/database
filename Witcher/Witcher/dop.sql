drop table #temp
CREATE TABLE #temp (DateOfAdded DATETIME2(0)); 
INSERT #temp (DateOfAdded) VALUES 
('2017-11-25 13:21:13'), 
('2018-11-30 13:38:47'),
('2017-11-26 13:21:18'), 
('2017-11-27 13:22:22'), 
('2017-11-29 13:23:58'), 
('2017-11-30 13:38:47'),
('2018-11-26 13:38:47'), 
('2017-12-10 13:39:13'), 
('2017-12-11 13:53:33'), 
('2017-12-12 13:53:42'), 
('2017-12-13 13:54:54'); 

SELECT * 
FROM #temp 
select distinct
min(DateOfAdded) over(PARTITION BY tmp), max(DateOfAdded) over(PARTITION BY tmp)
from
(select  *, 
		sum(mark) over(order by DateOfAdded) as tmp
from (select *, 
	  case when datediff(day, lag(DateOfAdded) over(order by DateOfAdded), DateOfAdded)>1 
		   then 1 else 0 END mark 
	  from #temp 
) x
) as x
CREATE PROCEDURE Getfactor(@num AS INT OUTPUT) AS
BEGIN
    declare @i int
	set @i = @num - 1;
    while @i > 0
	begin
		set @num = @num * @i;
		set @i = @i - 1;
	end
end

DROP PROCEDURE Getfactor;
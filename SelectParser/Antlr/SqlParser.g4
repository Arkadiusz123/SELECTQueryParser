grammar SqlParser;

stat
  : SELECT column_list FROM table_element JOIN table_element ON STRING
  ;

column_list
 : column (',' column)*
 ;

 table_element
 : table_name
 | table_name_with_alias
 ;

column
 : column_name
 | column_ratio
 | column_with_table_ref
 ;

column_name		        : NAME;
column_ratio		      : (column_name|column_with_table_ref) ' / ' (column_name|column_with_table_ref) AS NAME;
column_with_table_ref : NAME ('.' NAME);

table_name            : NAME ('.' NAME);
table_name_with_alias : table_name (' ' NAME);

SELECT                : [Ss][Ee][Ll][Ee][Cc][Tt];
FROM                  : [Ff][Rr][Oo][Mm];
JOIN                  : [Jj][Oo][Ii][Nn];
ON                    : [Oo][Nn];
AS                    : [Aa][Ss];
NAME                  : [A-Za-z] [A-Za-z0-9]*;
STRING                : '\'' .*? '\'';


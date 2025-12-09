# Code-Puzzle


Code Puzzle Solution.

* This Console application allows you to pass in a new input string as the first parameter, otherwise it will default to using the hardcoded input string in the Program.cs file.
* The Program.cs includes an example for changing that input string to include quotes for values that have an embedded comma, such as first, last name.
* In most cases the application will require an exact match on the number of fields, but does not do any validation on the contents of those fields.
* The one exception is that the customFields may contain any number of values, including 0.  The 'customFields' value itself is required, along with the (), but there can be 0 to many 'c1'/etc fields within that.
* For the 'type' and 'customFields' headers, the values entered there can be any value and the application will identify what they are based on the CSV location in the string.
* For fields other than 'type' and 'customFields' the values can contain parentheses and they will be treated as a part of the value itself, but they must include both the open and close parentheses to adhere to the schema requirements.


There are additional comments in the code, along with example TODO's that show what additional work could be done to flesh out the functionality or integrate it with a real solution.

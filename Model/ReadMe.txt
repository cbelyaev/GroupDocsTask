The class hierarchy for employees is implemented with inheritance.
Payroll is implemented as a separate service.

The advantages of the presented model are:
- compactness (thanks to inheritance);
- independence of employees from the payroll algorithm.

Recommendations for further development:
1) get rid of inheritance because of complexity of adding intermediate
(between employee and boss) categories of employees. Use the enum
field with the type of employee and extract boss/subordinates logic
to a separate class.

2) when adding employee data persistence in database there will be need
to split employee data classes into data access objects and data objects;

3) with the complexity of payroll algorithms may be required
to split SalaryCalculator class into separate strategies;

4) get rid of duplicate calculation of salaries for employees. Use
Dictionary <Employee, decimal> to memorize already calculated salaries.

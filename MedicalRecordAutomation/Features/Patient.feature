Feature: Patient
	In order to modify the patients records 
	as an admin
	I would like add, edit, delete patient records


Scenario: Verify Adding Valid Patient
	Given I have a browser with openemr application
	When I enter username as "admin"
	And I enter password as "pass"
	And I select language as "English (Indian)"
	And I click on login
	And I click on patient menu
	And I click on New-search menu
	And I fill the new patient form
		| firstname | middlename | lastname | gender | DOB        | licenseId |
		| John      | w          | Wick     | Male   | 2025-06-12 | sdj3j34j  |
	And I click on create new patient
	And I click on confirm create new patient
	And I store message and handle the alert box
	And I close the birthday popup if available
	Then I should verify the added name as "Medical Record Dashboard - John Wick"
	And I also the alert message should contains "Tobacco"
	And I verify the updated record in datatable
		

#Scenario Outline: Verify Adding Valid Patient2
#	Given I have a browser with openemr application
#	When I enter username as "<username>"
#	And I enter password as "<password>"
#	And I select language as "<language>"
#	And I click on login
#	And I click on patient menu
#	And I click on New-search menu
#	And I fill the new patient form
#		| firstname | middlename   | lastname | gender   | DOB   |
#		| <fname>   | <middlename> | <lname>  | <gender> | <dob> |
#	And I click on create new patient
#	And I click on confirm create new patient
#	And I store message and handle the alert box
#	And I close the birthday popup if available
#	Then I should verify the added name as "Medical Record Dashboard - <fname> <lastname>"
#	And I also the alert message should contains "Tobacco"
#Examples:
#	| username   | password   | language         | fname  | middlename | lname | gender | dob        |
#	| admin      | pass       | English (Indian) | john   | k          | woi   | Male   | 2025-06-12 |
#	| accountant | accountant | English (Indian) | john33 | 2k         | woi   | Female | 2025-06-12 |

﻿Feature: Login
In order to manage the medical records 
As a user
I want to login into the openemr dashboard 

Background:
	Given I have a browser with openemr application

Scenario Outline: Valid Login
	When I enter username as "<username>"
	And I enter password as "<password>"
	And I select language as "<language>"
	And I click on login
	Then I should get access to portal with title as "<expected_title>"
Examples:
	| username   | password   | language         | expected_title |
	| admin      | pass       | English (Indian) | OpenEMR        |
	| accountant | accountant | English (Indian) | OpenEMR        |

Scenario: Invalid Login
	When I enter username as "john"
	And I enter password as "john123"
	And I select language as "English (Indian)"
	And I click on login
	Then I should get not get access to portal with error "Invalid username or password"


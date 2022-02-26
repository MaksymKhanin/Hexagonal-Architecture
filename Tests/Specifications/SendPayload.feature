Feature: SendPayload
	As a customer, I must send my payload in order for the Third Party Service to be aware of it

@mytag
Scenario: As a customer, I must send my payload in order for the Third Party Service to be aware of it
	Given I have created my payload
	When I send my payload to the payload platform
	Then the payload platform has taken my payload into account
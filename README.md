# BetService

<h1>Competition Service</h1>
Provides bet related data.
Provides creation and update functionality.

<h2>API Methods</h2>

| Method              | Description                      |
| ------------------- | -------------------------------- |
| CreateBet()         | Creates the bet                  |
| CreateBetRange()    | Creates the bet range.           |
| UpdateBetStatuses() | Updates the bet statuses.        |
| GetUsersBets()      | Gets the users bets.             |
| GetUserBetById()    | Gets the user bet by identifier. |

<h2>Entity examples</h2>

Example of bet entity:

| Name           | Type          |
| -------------- | ------------- |
| Id             | Guid          |
| UserId         | Guid          |
| CoefficientId  | Guid          |
| Amount         | double        |
| Rate           | double        |
| CreateAtUtc    | DateTime      |
| PayoutStatus   | BetPaidType   |
| BetStatusType  | BetStatusType |

<h2>Requests examples</h2>
CreateBetRequset:

```
{
	"bet_create_model" : {
		"user_id" : "9a1e7016-77d3-47ab-84c2-0b3a1313b9b6",
		"coefficient_id" : "127de191-d7a7-434c-874c-60f93595ef91",
		"amount" : 100.0,
		"rate" : 1.4
	}
}
```

CreateBetRangeRequest:

```
{
	"bet_create_models" : [
		{
			"user_id" : "9a1e7016-77d3-47ab-84c2-0b3a1313b9b6",
			"coefficient_id" : "c3180015-1cfd-4abd-9247-807575235bc8",
			"amount" : 100.0,
			"rate" : 1.4
		},
		{
			"user_id" : "9a1e7016-77d3-47ab-84c2-0b3a1313b9b6",
			"coefficient_id" : "116d0470-20e2-445f-8049-3783c58bbaa7",
			"amount" : 100.0,
			"rate" : 1.4
		},
		{
			"user_id" : "9a1e7016-77d3-47ab-84c2-0b3a1313b9b6",
			"coefficient_id" : "3e907b46-ed61-4c4f-ab65-4fd6ae47a6f7",
			"amount" : 100.0,
			"rate" : 1.4
		}
	]
}
```

GetUserBetByIdRequset:

```
{
	"id" : "999d37d5-a15d-4469-9373-aaa4119b8c59"
}
```

GetUsersBetsRequset:

```
{
	"user_id" : "9a1e7016-77d3-47ab-84c2-0b3a1313b9b6",
	"page" : 1,
	"page_size" : 5
}
```

UpdateBetStatusesRequest:

```
{
	"bet_status_update_models" : [
		{
			"coefficient_id" : "3e907b46-ed61-4c4f-ab65-4fd6ae47a6f7",
			"status_type" : "BET_STATUS_TYPE_WIN"
		},
		{
			"coefficient_id" : "116d0470-20e2-445f-8049-3783c58bbaa7",
			"status_type" : "BET_STATUS_TYPE_LOSE"
		},
		{
			"coefficient_id" : "c3180015-1cfd-4abd-9247-807575235bc8",
			"status_type" : "BET_STATUS_TYPE_CANCELED"
		},
		{
			"coefficient_id" : "c3180015-1cfd-4abd-9247-807575235bc8",
			"status_type" : "BET_STATUS_TYPE_WIN"
		}
	]
}
```

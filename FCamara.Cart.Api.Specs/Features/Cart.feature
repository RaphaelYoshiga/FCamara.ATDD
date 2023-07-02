Feature: Cart price calculation
Example of how a pricing calculator can be described in Specflow

Background:
	Given I we have this catalogue of products
		| Id                                   | Price | Name     | Description                 |
		| 3d1800b2-779d-4f0f-82b6-93dd88ac1e30 | 1.99  | Blue Pen | Blue Pen, great for writing |
		| 2181cf26-a138-4ac5-b62e-9b17c1ea2d94 | 3.05  | Pencil   | Standard Pencil             |

Scenario: Calculate cart total price
	When we ask for the price of this cart
		"""
		{
		  "items":[{
			"productId": "3d1800b2-779d-4f0f-82b6-93dd88ac1e30",
			"quantity": 3
		  }]
		}
		"""
	Then the response should be 200 OK
	And the cart response should contain those items
		| ProductId                            | Quantity | UnitPrice | TotalPrice | Name     | Description                 |
		| 3d1800b2-779d-4f0f-82b6-93dd88ac1e30 | 3        | 1.99      | 5.97       | Blue Pen | Blue Pen, great for writing |
	And the cart total price is 5.97

Scenario: Calculate cart total price for multiple items
	When we ask for the price of this cart
		"""
		{
		  "items":[{
			"productId": "3d1800b2-779d-4f0f-82b6-93dd88ac1e30",
			"quantity": 3
		  },
		  {
			"productId": "2181cf26-a138-4ac5-b62e-9b17c1ea2d94",
			"quantity": 2
		  }]
		}
		"""
	Then the response should be 200 OK
	And the cart response should contain those items
		| ProductId                            | Quantity | UnitPrice | TotalPrice | Name     | Description                 |
		| 3d1800b2-779d-4f0f-82b6-93dd88ac1e30 | 3        | 1.99      | 5.97       | Blue Pen | Blue Pen, great for writing |
		| 2181cf26-a138-4ac5-b62e-9b17c1ea2d94 | 2        | 3.05      | 6.10       | Pencil   | Standard Pencil             |
	And the cart total price is 12.07

Scenario: Calculate cart for unknown product id
	When we ask for the price of this cart
		"""
		{
		  "items":[
		  {
			"productId": "b6fe13f6-e8a1-4f1d-a199-3c61b85ddc62",
			"quantity": 2
		  }]
		}
		"""
	Then the response should be 400 Bad Request

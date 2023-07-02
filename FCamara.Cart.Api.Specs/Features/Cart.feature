Feature: Cart price calculation
Example of how a pricing calculator can be described in Specflow
Scenario: Calculate cart total price
	Given I we have this catalogue of products
	| Id                                   | Price | Name     | Description                 |
	| 3d1800b2-779d-4f0f-82b6-93dd88ac1e30 | 1.99  | Blue Pen | Blue Pen, great for writing |
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
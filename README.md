# Payment Gateway

## Requirements

1. Create a way for a merchant to
    - Process payments via the payment gateway
2. The payment gateway must be able to handle payments that return
    - Successful responses
    - Unsuccessful responses
3. Create a way for merchants to retrieve details of a previously made payment
    - Retrieve all past payments
    - Retrieve past payment by unique identifier


### Addons

1. Create an API to simulate interacting with a real Bank API
    - Create API for validating card details
    - Create API to secure requests for card detail validation

2. Collect log data for use when identify metrics.
    - API health
    - Request Statuses from Bank API
    - Failed requests from Payments Gateway

3. Containerize APIs to simply development and testing

4. Utilize github actions to practise cicd

5. only store mandatory details in data stores.

6. Validate incoming requests

7. Generate swagger documents to document APIs

# MovieRental Exercise

A small, self-contained demo of a movie rental system used for bug fixes and feature exercises.

## Overview

This exercise contains a few bugs and missing features to implement. Below are the tasks and the current answers/notes included in the project (left by the previous contributor). Please implement fixes and improvements where needed.

## Tasks & Notes

- **Startup error** — The app throws an error on startup.  
  **Answer:** The DbContext was registered with a *Scoped* lifetime while `RentalFeatures` was registered as a *Singleton* and depended on the DbContext. This service-lifetime mismatch caused the error. I changed `RentalFeatures` to *Scoped* so it gets one instance per request, matching the DbContext.

- **Make save method asynchronous** — The rental save method was not async.  
  **Answer:** Converting the save method to `async` and using `SaveChangesAsync` prevents blocking the thread while waiting for I/O (database) operations, which improves scalability and responsiveness. Using `await` ensures the method completes only after the save operation finishes.

- **Filter rentals by customer name** — Implemented and a new endpoint added.  
  **Status:** *Done*

- **Add `Customer` entity** — A `Customer` table was missing; having only the customer name on the `Rental` entity is not ideal.  
  **Status:** *Done* (the `Rental` customer name was replaced with a foreign key to `Customer`, and the filter method was updated accordingly)

- **Movie listing method review** — There is a method that lists all movies.  
  **Answer:** Returning all movies without pagination or filtering can cause performance issues on large datasets. Consider returning `IQueryable<Movie>` or `IEnumerable<Movie>` and adding pagination, filtering, and proper error handling/logging.

- **Exception handling** — No exceptions are being caught in the API.  
  **Answer:** Use targeted `try/catch` blocks where necessary and validate inputs before performing operations to reduce unnecessary error handling. Add logging to improve observability and clearer error responses for clients.
## Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.

# MovieRental Exercise

A small, self-contained demo of a movie rental system used for bug-fixing and feature implementation exercises.

## Overview

This project contains several intentional bugs and missing features. Below is a list of the proposed tasks along with notes and implementations added by a previous contributor. Where applicable, fixes and improvements have been implemented and documented.

## Tasks & Notes

- **Startup error** — The application throws an error on startup.  
  **Answer:** The `DbContext` was registered with a *Scoped* lifetime, while `RentalFeatures` was registered as a *Singleton* and depended on it. This service lifetime mismatch caused the error.  
  `RentalFeatures` was changed to *Scoped*, ensuring it receives one instance per request and aligns with the `DbContext` lifetime.  
  [Program.cs line 20]

- **Make save method asynchronous** — The rental save method was synchronous.  
  **Answer:** The method was converted to `async`, using `SaveChangesAsync` to avoid blocking the thread during I/O-bound database operations. This improves scalability and responsiveness. The use of `await` ensures the operation completes before returning.  
  [RentalFeatures.cs line 11]

- **Filter rentals by customer name** — Implement filtering and expose a new endpoint.  
  **Answer:** A simple LINQ query was implemented to maintain readability and flexibility, allowing the result to be reused as different `IEnumerable` types if needed.  
  An asynchronous version could also be added; however, supporting both synchronous and asynchronous approaches can be considered good practice depending on usage context.  
  [RentalFeatures.cs line 22]

- **Add `Customer` entity** — The system previously stored only a customer name on the `Rental` entity.  
  **Status:** *Done*. A `Customer` entity and table were added, and the `Rental` entity now references it via a foreign key. The filtering logic was updated accordingly.  
  [Rental.cs line 13]

- **Movie listing method review** — A method exists that returns all movies.  
  **Answer:** Returning the full dataset without pagination or filtering can lead to performance issues with larger datasets. A better approach would be to return an `IQueryable<Movie>` or `IEnumerable<Movie>` and support pagination, filtering, and proper error handling and logging.

- **Exception handling** — No exception handling was present in the API.  
  **Answer:** Introduce targeted `try/catch` blocks where appropriate and validate inputs before performing operations to minimize unnecessary exception handling. Adding structured logging would also improve observability and provide clearer error responses to API consumers.

## Challenge — Implemented

The **Factory Method** was implemented to decouple rental fee payments from concrete `PaymentProvider` implementations. The system now depends solely on a common payment interface, allowing payment processing without introducing additional conditional logic or tight coupling.

The interface requires only:
- The selected `paymentMethod` (defined as an enum)
- The amount to be paid

While each payment provider may differ in its internal implementation, the required input data remains consistent across all payment methods.

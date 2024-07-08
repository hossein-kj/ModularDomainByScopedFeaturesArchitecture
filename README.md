# Modular Domain By Scoped Features Architecture
![demo](https://github.com/hossein-kj/ModularDomainByScopedFeaturesArchitecture/assets/13397236/121ceb0e-01d3-4c44-af23-c8cb93b48a3e)

The Goals of This Project
+❇️ Using Mix of Vertical Slice Architecture and Modular Monolith Architecture for architecture level.
+❇️ Write Code Once , and support Monolith and MicroService at sameTime(by a little change).
+❇️ Support Windows Form Application and Blazor Web Application And Web Api Appliction and Mvc WebApplicaton At SameTime.
+❇️ Using Domain Driven Design (DDD) to implement all business processes in modules and Features.
+❇️ Using InMememoryBroker on top of Cap for Event Driven Architecture between our modules.
+❇️ Using Inbox Pattern on top of Cap for ensuring message idempotency for receiver and Exactly once Delivery.
+❇️ Using Outbox Pattern on top of Cap for ensuring no message is lost and there is at Least One Delivery.
+❇️ Using CQRS implementation with MediatR library.
+❇️ Using SqlServer for database in our modules.
+❇️ Using Fluent Validation and a Validation Pipeline Behaviour on top of MediatR.

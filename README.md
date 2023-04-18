# Trello Clone

Backend in C# with Entity framework and MS SQL server, Uses JWT Bearer token for authentication.

Entities:
1. User
2. Kanban Board
3. Board List
4. Card
5. Comment

Link tables(Many to many):
Membership (User, Kanban board)
Assignment(User, Card)


Controllers:
-User controller
  Allows getting all users (testing purposes), getting specific user (auth), Registering(no auth), Deleting (auth, admin only), updating user (no auth - TODO)
  
-Login controller
  Authenticates login and generates JWT token and returns the token, used for all authenticated actions, tokens last 20 minutes.
  
-Master board controller
  A consolidated controller for crud actions for Kanban board, Board list, Card, Comments, Memberships and Assignments.
    -All actions except testing actions (get all for example) require authentication of JWT token, and additionally require access, i.e to perform actions on   board X, the JWT token provided must belong to a user already a member of said board.
    
    
Frontend - TODO.

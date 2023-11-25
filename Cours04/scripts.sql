CREATE TABLE Users (
    [UserId] INT PRIMARY KEY IDENTITY(1, 1),
    [UserName] NVARCHAR(50) NOT NULL
);

CREATE TABLE TodoItems (
    [TodoItemId] INT PRIMARY KEY IDENTITY(1, 1),
    [Description] NVARCHAR(200) NOT NULL,
    [IsCompleted] BIT NOT NULL DEFAULT 0,
    [UserId] INT FOREIGN KEY REFERENCES Users(UserId),
    [ParentTodoItemId] INT NULL,
    CONSTRAINT FK_TodoItems_Parent FOREIGN KEY (ParentTodoItemId) REFERENCES TodoItems(TodoItemId)
);

CREATE TABLE [dbo].[Posts] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [CreatorId]     NVARCHAR (450) NULL,
    [Title]         NVARCHAR (MAX) NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [CreatedOn]     DATETIME2 (7)  NOT NULL,
    [UpdatedOn]     DATETIME2 (7)  NOT NULL,
    [Published]     BIT            NOT NULL,
    [Approved]      BIT            NOT NULL,
    [ApproverId]    NVARCHAR (450) NULL,
    [DislikesCount] INT             NOT NULL,
    [LikesCount]    INT             NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Posts_AspNetUsers_ApproverId] FOREIGN KEY ([ApproverId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Posts_AspNetUsers_CreatorId] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_ApproverId]
    ON [dbo].[Posts]([ApproverId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_CreatorId]
    ON [dbo].[Posts]([CreatorId] ASC);


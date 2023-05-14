CREATE TABLE [dbo].[Likes] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [PostId]     INT            NOT NULL,
    [UserId]     NVARCHAR (MAX) NULL,
    [IsLiked]    BIT            NOT NULL,
    [IsDisliked] BIT            NOT NULL,
    CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


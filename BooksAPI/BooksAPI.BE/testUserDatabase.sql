INSERT INTO public."AspNetUsers" ("Id", "RefreshToken", "RefreshTokenExpiry", "UserName", "NormalizedUserName", "Email",
                                  "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp",
                                  "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled",
                                  "LockoutEnd", "LockoutEnabled", "AccessFailedCount")
VALUES ('31643fae-cec0-4756-bf85-aa333bee61b4', NULL, '-infinity', 'personalProfile', 'PERSONALPROFILE',
        'user1@example.com', 'USER1@EXAMPLE.COM', false,
        'AQAAAAIAAYagAAAAEHSBGq1g75CU0f1u+UvYIFjrQQQ7jhj8Vg0eJA80gUHuVOzidur/+9/5P+wTSuxIaQ==',
        'VEOUNPE2ZYZGNV4BQA4SEOEREKHS7MVL', 'a12c2c91-a720-429b-b9a5-e948e2fa9366', NULL, false, false, NULL, true, 0);

INSERT INTO public."AspNetUserRoles" ("UserId", "RoleId")
VALUES ('31643fae-cec0-4756-bf85-aa333bee61b4', '3187bce0-f9a9-48fb-adb6-36cea86dfb16'),
       ('31643fae-cec0-4756-bf85-aa333bee61b4', '284b3a5c-4235-4b01-ba23-09f2f6f9737c');

INSERT INTO public."UserMangas" ("Id","ReadingStatus","ReadVolumes","CollectedVolumes","PricePerVolume","CollectionStatus","UserId","LibraryMangaId") VALUES
	 (1,'PlanToRead',0,1,36,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',1168),
	 (2,'Reading',17,17,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',118),
	 (3,'Reading',10,10,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',23),
	 (4,'Finished',23,23,18,'Collected','31643fae-cec0-4756-bf85-aa333bee61b4',80),
	 (5,'Reading',13,16,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',8),
	 (6,'Reading',14,14,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',9),
	 (7,'Reading',14,14,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',172),
	 (8,'Reading',21,21,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',26),
	 (9,'Finished',1,1,18,'Collected','31643fae-cec0-4756-bf85-aa333bee61b4',159),
	 (10,'Finished',2,2,18,'Collected','31643fae-cec0-4756-bf85-aa333bee61b4',1379),
	 (11,'Reading',11,11,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',281),
	 (12,'Reading',6,6,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',113),
	 (13,'Reading',4,5,26.50,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',27),
	 (14,'Finished',7,7,19.9,'Collected','31643fae-cec0-4756-bf85-aa333bee61b4',11),
	 (15,'Finished',11,11,19.9,'Collected','31643fae-cec0-4756-bf85-aa333bee61b4',56),
	 (16,'Reading',3,4,22,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',980),
	 (17,'PlanToRead',0,1,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',287),
	 (18,'Reading',3,3,18,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',64),
	 (19,'PlanToRead',0,1,20.9,'InProgress','31643fae-cec0-4756-bf85-aa333bee61b4',31),
	 (20,'PlanToRead',0,0,28,'PlanToCollect','31643fae-cec0-4756-bf85-aa333bee61b4',193);
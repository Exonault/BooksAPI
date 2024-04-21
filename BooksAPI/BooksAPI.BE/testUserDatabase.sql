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

-- INSERT INTO public."UserMangas" ("Id", "ReadingStatus", "ReadVolumes", "CollectedVolumes", "PricePerVolume",
--                                  "CollectionStatus", "UserId", "LibraryMangaId")
-- VALUES ('ddc18308-0565-41b6-a700-3802b906705b', 'Finished', 23, 23, 18, 'Collected',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '911602f7-4f5c-4b7f-bab3-af08180afb7d'),
--        ('4e09fcd3-341e-4a63-82e1-dad9f87c80ce', 'Reading', 17, 17, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '33e7d891-f5be-4491-9010-39102adc1fec'),
--        ('2687ba66-b8ee-4d21-87a2-b1ee6a4f97d3', 'Reading', 10, 10, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', 'd5e57678-c893-4668-bc25-6ab20bc175ad'),
--        ('e859ddd8-0894-46c0-9632-75ec929412e7', 'OnHold', 13, 17, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '74d186d6-ff17-44ca-91a7-d7df89d23f00'),
--        ('c54b5384-6650-45ff-8126-bbc7eb00940d', 'Reading', 14, 14, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '79abd909-7299-43d3-8155-b63a2a4fc0b8'),
--        ('231c3f56-4838-472c-8794-3dc763e4c77f', 'Reading', 14, 14, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '54ce8b4c-514b-4e6e-9ea9-048558c2bba4'),
--        ('e0799190-8c57-4b05-8a3b-4b43988510e9', 'Reading', 21, 21, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '2ecd6135-24a1-44ca-aa71-66d55998e4c0'),
--        ('4dd98dbc-d92a-4d48-a10c-75622e112170', 'Finished', 1, 1, 18, 'Collected',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', 'aeaff6d6-512c-4990-9d7c-d0610d16dd28'),
--        ('6c83b79b-7759-44ac-9bec-72461f5204cb', 'Reading', 4, 4, 26.50, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '04376d1a-727e-4d3b-b043-658ec8f12e22'),
--        ('947dfea9-8f13-42b3-9967-7fb45e0f09cf', 'Finished', 7, 7, 19.9, 'Collected',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '9e3766d4-2adb-41b3-9a56-90c0fe9a3286');
-- INSERT INTO public."UserMangas" ("Id", "ReadingStatus", "ReadVolumes", "CollectedVolumes", "PricePerVolume",
--                                  "CollectionStatus", "UserId", "LibraryMangaId")
-- VALUES ('7544e222-dcca-4e7c-b286-9998f63c3207', 'Finished', 11, 11, 19.9, 'Collected',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', 'b3573a2a-3174-4d0d-89af-12de9871381b'),
--        ('302d5636-97bc-47e2-aaef-42a6c9d0b639', 'Reading', 3, 3, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '171aebf4-b880-44d5-b0ec-e9331fb9f25d'),
--        ('618fa3dd-b02a-42d1-8b46-32683c89dfec', 'PlanToRead', 0, 1, 20.9, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', 'e5bd08b7-494b-4c59-a4e1-27fde6ea1c82'),
--        ('784fef18-bee5-4eb3-b86e-85d0a4c6ffb0', 'Reading', 6, 6, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', 'e7a05057-58f4-4908-88b4-881117716b8c'),
--        ('378a5083-cf09-4992-b7ac-4ec98fb69b5e', 'PlanToRead', 0, 1, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '6f804b38-9599-4355-a811-32f2a31c372d'),
--        ('bd735709-b97a-484e-9f3c-77aea0ded684', 'Reading', 11, 11, 18, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '27ba4658-bed4-4317-a176-58841a5c178b'),
--        ('10284490-fe5b-4519-bd9f-39a902762cb6', 'PlanToRead', 0, 1, 36, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', '4b858b89-1965-44cc-8f5d-7c449548fdc7'),
--        ('b0a4b268-8cd1-4bee-bc57-3de143829e86', 'PlanToRead', 0, 0, 26.80, 'PlanToCollect',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', 'a16d9867-e877-415b-8e8d-f0dddf020379'),
--        ('afe316b8-3815-453b-8239-369d584e3a84', 'Reading', 4, 4, 20, 'InProgress',
--         '31643fae-cec0-4756-bf85-aa333bee61b4', 'fb41d5df-50f0-4bab-bdd3-a22f7fb54031');
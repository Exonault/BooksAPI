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

INSERT INTO public."UserMangas" 
    ("ReadingStatus", "ReadVolumes", "CollectedVolumes", "PricePerVolume", "CollectionStatus", "UserId", "LibraryMangaId")
VALUES ('PlanToRead', 0, 1, 36, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 1168),
       ('Reading', 17, 17, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 118),
       ('Reading', 10, 10, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 23),
       ('Finished', 23, 23, 18, 'Collected', '31643fae-cec0-4756-bf85-aa333bee61b4', 80),
       ('OnHold', 13, 16, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 8),
       ('Reading', 14, 14, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 9),
       ('Reading', 14, 14, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 172),
       ('Reading', 21, 21, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 26),
       ('Finished', 1, 1, 18, 'Collected', '31643fae-cec0-4756-bf85-aa333bee61b4', 159),
       ('Finished', 2, 2, 18, 'Collected', '31643fae-cec0-4756-bf85-aa333bee61b4', 1379),
       ('Reading', 11, 11, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 281),
       ('Reading', 6, 6, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 113),
       ('Reading', 4, 5, 26.50, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 27),
       ('Finished', 7, 7, 19.9, 'Collected', '31643fae-cec0-4756-bf85-aa333bee61b4', 11),
       ('Finished', 11, 11, 19.9, 'Collected', '31643fae-cec0-4756-bf85-aa333bee61b4', 56),
       ('Reading', 3, 4, 22, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 980),
       ('PlanToRead', 0, 1, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 287),
       ('Reading', 3, 3, 18, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 64),
       ('PlanToRead', 0, 1, 20.9, 'InProgress', '31643fae-cec0-4756-bf85-aa333bee61b4', 31),
       ('PlanToRead', 0, 0, 28, 'PlanToCollect', '31643fae-cec0-4756-bf85-aa333bee61b4', 193),
       ('PlanToRead', 0, 0, 20, 'PlanToCollect', '31643fae-cec0-4756-bf85-aa333bee61b4', 1601);


INSERT INTO public."Orders" ("Date", "Description", "Place", "Amount", "NumberOfItems", "UserId")
VALUES ('2024-01-28','Chainsaw man vol 13; Kubo won''t let me be invisible vol 10; Frieren beyond journeys end vol 3', 'Bookholic', 56, 3, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ('2024-03-17', 'Call of the night vol 14', 'Ozone', 18, 1, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ('2024-04-01', 'Chainsaw man vol 14; Kubo won''t let me be invisible vol 11; DanDaDan vol 6; ', 'Bookholic', 60, 3, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ('2024-04-22', 'Boys Abyss vol 3; Oshi no Ko vol 5; ', 'Orange', 39.89, 2, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ('2024-01-22', 'Boys Abyss vol 3; Oshi no Ko vol 4 ', 'Orange', 48.9, 2, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ('2023-01-15', 'DaDaDan vol 1; Komi can''t communicate vol 11', 'In person', 36, 2,   '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ('2023-02-20', 'DanDaDan vol 2; Call of the night vol 10; Oshi no ko vol 1; Jujutsu Kaisen vol 18', 'Amazon',  82, 4, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ('2023-03-14', 'A silent voice box set; Kubo won''t let me be invisible vol 4 and 5 ', 'Amazon', 160, 3,  '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ('2023-04-10', 'Spy x Family vol 9; Jujutsu Kaisen vol 19; Kubo won''t let me be invisible vol 6', 'Amazon',   57.50, 3, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ( '2023-05-08', 'Your lie in april vol 1,2,9,10,11', 'Amazon', 117.40, 5,   '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ( '2023-05-11', 'Komi can''t communicate vol 12,13,14,15,16', 'Ozone', 65.7, 5,     '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ( '2023-05-23', 'Oshi no ko vol 2; Call of the night vol 11; DanDaDan vol 3;', 'Amazon', 63.80, 3,     '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ( '2023-06-06', 'Boy''s Abyss vol 1', 'Amazon', 23.5, 1, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ( '2023-06-23', 'Your lie in april vol 3,4,5,6', 'Knigomania', 60, 4, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ( '2023-07-03', 'Your lie in april vol 7,8', 'Amazon', 46.71, 2, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ( '2023-07-10', 'Call of the night vol 12; Kubo won''t let me be invisible vol 7; Komi can''t communicate vol 17 ', 'Bookholic',  54, 3, '31643fae-cec0-4756-bf85-aa333bee61b4'),
       ( '2023-08-20', 'Oshi no ko vol 3', 'Amazon', 8.60, 1, '31643fae-cec0-4756-bf85-aa333bee61b4');

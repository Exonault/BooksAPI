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

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Account" (
        id uuid NOT NULL,
        username text NOT NULL,
        password text NOT NULL,
        email text NOT NULL,
        role integer NOT NULL,
        is_active boolean NOT NULL,
        img_url text,
        otp text,
        "otpExpired" timestamp with time zone,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Account" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Pet_Type" (
        id uuid NOT NULL,
        name text,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Pet_Type" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Service_Type" (
        id uuid NOT NULL,
        name text,
        description text,
        img_url text,
        is_public boolean NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Service_Type" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Customer" (
        account_id uuid NOT NULL,
        full_name text,
        gender text,
        birthday timestamp with time zone,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Customer" PRIMARY KEY (account_id),
        CONSTRAINT "FK_Customer_Account_account_id" FOREIGN KEY (account_id) REFERENCES "Account" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Notification" (
        id uuid NOT NULL,
        receiver_id uuid NOT NULL,
        description text,
        is_read boolean NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Notification" PRIMARY KEY (id),
        CONSTRAINT "FK_Notification_Account_receiver_id" FOREIGN KEY (receiver_id) REFERENCES "Account" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Shop" (
        account_id uuid NOT NULL,
        name text,
        description text,
        status boolean NOT NULL,
        working_day text,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Shop" PRIMARY KEY (account_id),
        CONSTRAINT "FK_Shop_Account_account_id" FOREIGN KEY (account_id) REFERENCES "Account" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Appointments" (
        id uuid NOT NULL,
        customer_id uuid NOT NULL,
        location_type text,
        status boolean,
        notes text,
        start_time timestamp with time zone NOT NULL,
        end_type timestamp with time zone NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Appointments" PRIMARY KEY (id),
        CONSTRAINT "FK_Appointments_Customer_customer_id" FOREIGN KEY (customer_id) REFERENCES "Customer" (account_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Rating" (
        id uuid NOT NULL,
        customer_id uuid NOT NULL,
        star real,
        comment text,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Rating" PRIMARY KEY (id),
        CONSTRAINT "FK_Rating_Customer_customer_id" FOREIGN KEY (customer_id) REFERENCES "Customer" (account_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Room" (
        id uuid NOT NULL,
        name text,
        description text,
        price double precision,
        service_type_id uuid NOT NULL,
        shop_id uuid NOT NULL,
        pet_type_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Room" PRIMARY KEY (id),
        CONSTRAINT "FK_Room_Pet_Type_pet_type_id" FOREIGN KEY (pet_type_id) REFERENCES "Pet_Type" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Room_Service_Type_service_type_id" FOREIGN KEY (service_type_id) REFERENCES "Service_Type" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Room_Shop_shop_id" FOREIGN KEY (shop_id) REFERENCES "Shop" (account_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Service" (
        id uuid NOT NULL,
        name text NOT NULL,
        is_active boolean NOT NULL,
        shop_id uuid NOT NULL,
        description text NOT NULL,
        discount_percent real NOT NULL,
        "Price" double precision NOT NULL,
        limit_per_hour integer NOT NULL,
        pet_type_id uuid NOT NULL,
        duration_type integer NOT NULL,
        "Star" real NOT NULL,
        purchases integer NOT NULL,
        service_type_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Service" PRIMARY KEY (id),
        CONSTRAINT "FK_Service_Pet_Type_pet_type_id" FOREIGN KEY (pet_type_id) REFERENCES "Pet_Type" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Service_Service_Type_service_type_id" FOREIGN KEY (service_type_id) REFERENCES "Service_Type" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Service_Shop_shop_id" FOREIGN KEY (shop_id) REFERENCES "Shop" (account_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Staff" (
        account_id uuid NOT NULL,
        shop_id uuid NOT NULL,
        full_name text,
        gender text,
        position text,
        birthday timestamp with time zone,
        hired_at text,
        shop_address_id text,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Staff" PRIMARY KEY (account_id),
        CONSTRAINT "FK_Staff_Account_account_id" FOREIGN KEY (account_id) REFERENCES "Account" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Staff_Shop_shop_id" FOREIGN KEY (shop_id) REFERENCES "Shop" (account_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Sub_Address" (
        id uuid NOT NULL,
        shop_id uuid NOT NULL,
        phone integer,
        address_name text NOT NULL,
        is_default boolean NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Sub_Address" PRIMARY KEY (id),
        CONSTRAINT "FK_Sub_Address_Shop_shop_id" FOREIGN KEY (shop_id) REFERENCES "Shop" (account_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "ImageGallery" (
        id uuid NOT NULL,
        name text,
        img_url text,
        service_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_ImageGallery" PRIMARY KEY (id),
        CONSTRAINT "FK_ImageGallery_Service_service_id" FOREIGN KEY (service_id) REFERENCES "Service" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE TABLE "Service_Appointment" (
        service_id uuid NOT NULL,
        start_time timestamp with time zone NOT NULL,
        end_type timestamp with time zone NOT NULL,
        appointment_id uuid NOT NULL,
        rating_id uuid NOT NULL,
        room_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Service_Appointment" PRIMARY KEY (service_id),
        CONSTRAINT "FK_Service_Appointment_Appointments_appointment_id" FOREIGN KEY (appointment_id) REFERENCES "Appointments" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Service_Appointment_Rating_rating_id" FOREIGN KEY (rating_id) REFERENCES "Rating" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Service_Appointment_Room_room_id" FOREIGN KEY (room_id) REFERENCES "Room" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Service_Appointment_Service_service_id" FOREIGN KEY (service_id) REFERENCES "Service" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE UNIQUE INDEX "IX_Account_email" ON "Account" (email);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE UNIQUE INDEX "IX_Account_username" ON "Account" (username);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Appointments_customer_id" ON "Appointments" (customer_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_ImageGallery_service_id" ON "ImageGallery" (service_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Notification_receiver_id" ON "Notification" (receiver_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Rating_customer_id" ON "Rating" (customer_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Room_pet_type_id" ON "Room" (pet_type_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Room_service_type_id" ON "Room" (service_type_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Room_shop_id" ON "Room" (shop_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Service_pet_type_id" ON "Service" (pet_type_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Service_service_type_id" ON "Service" (service_type_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Service_shop_id" ON "Service" (shop_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Service_Appointment_appointment_id" ON "Service_Appointment" (appointment_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE UNIQUE INDEX "IX_Service_Appointment_rating_id" ON "Service_Appointment" (rating_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Service_Appointment_room_id" ON "Service_Appointment" (room_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Staff_shop_id" ON "Staff" (shop_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    CREATE INDEX "IX_Sub_Address_shop_id" ON "Sub_Address" (shop_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250601122524_First') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250601122524_First', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250608045145_img') THEN
    ALTER TABLE "Account" ADD img_url_id text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250608045145_img') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250608045145_img', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "ImageGallery" DROP CONSTRAINT "FK_ImageGallery_Service_service_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" DROP CONSTRAINT "FK_Room_Pet_Type_pet_type_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" DROP CONSTRAINT "FK_Room_Service_Type_service_type_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" DROP CONSTRAINT "FK_Room_Shop_shop_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Service" DROP CONSTRAINT "FK_Service_Pet_Type_pet_type_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    DROP INDEX "IX_Service_pet_type_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    DROP INDEX "IX_Room_pet_type_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    DROP INDEX "IX_Room_service_type_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    DROP INDEX "IX_Room_shop_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    DROP INDEX "IX_ImageGallery_service_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Service" DROP COLUMN pet_type_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" DROP COLUMN description;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" DROP COLUMN pet_type_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" DROP COLUMN service_type_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" DROP COLUMN shop_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "ImageGallery" DROP COLUMN service_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" RENAME COLUMN price TO daily_price;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" RENAME COLUMN name TO amendities;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Sub_Address" ADD name text NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" ADD "Hotelid" uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" ADD is_available boolean NOT NULL DEFAULT FALSE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" ADD max_capacity integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" ADD room_number integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" ADD room_type integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" ADD star integer;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "ImageGallery" ADD owner_id text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE TABLE "Hotel" (
        id uuid NOT NULL,
        name text,
        description text,
        total_room integer,
        available_room integer,
        shop_id uuid NOT NULL,
        sub_address_id uuid,
        is_active boolean NOT NULL,
        CONSTRAINT "PK_Hotel" PRIMARY KEY (id),
        CONSTRAINT "FK_Hotel_Shop_shop_id" FOREIGN KEY (shop_id) REFERENCES "Shop" (account_id) ON DELETE CASCADE,
        CONSTRAINT "FK_Hotel_Sub_Address_sub_address_id" FOREIGN KEY (sub_address_id) REFERENCES "Sub_Address" (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE TABLE "Pet_Service_Room" (
        id uuid NOT NULL,
        owner_id uuid NOT NULL,
        pet_type_id uuid NOT NULL,
        is_service boolean NOT NULL,
        service_id uuid NOT NULL,
        room_id uuid NOT NULL,
        CONSTRAINT "PK_Pet_Service_Room" PRIMARY KEY (id),
        CONSTRAINT "FK_Pet_Service_Room_Pet_Type_pet_type_id" FOREIGN KEY (pet_type_id) REFERENCES "Pet_Type" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Pet_Service_Room_Room_room_id" FOREIGN KEY (room_id) REFERENCES "Room" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Pet_Service_Room_Service_service_id" FOREIGN KEY (service_id) REFERENCES "Service" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE TABLE "Room_Booking" (
        id uuid NOT NULL,
        room_detail_id uuid NOT NULL,
        customer_id uuid NOT NULL,
        check_in_date timestamp with time zone NOT NULL,
        check_out_date timestamp with time zone NOT NULL,
        total_night integer NOT NULL,
        total_amount integer NOT NULL,
        feeding_schedule timestamp with time zone NOT NULL,
        medication_schedule timestamp with time zone NOT NULL,
        status boolean NOT NULL,
        CONSTRAINT "PK_Room_Booking" PRIMARY KEY (id),
        CONSTRAINT "FK_Room_Booking_Customer_customer_id" FOREIGN KEY (customer_id) REFERENCES "Customer" (account_id) ON DELETE CASCADE,
        CONSTRAINT "FK_Room_Booking_Room_room_detail_id" FOREIGN KEY (room_detail_id) REFERENCES "Room" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE INDEX "IX_Room_Hotelid" ON "Room" ("Hotelid");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE INDEX "IX_Hotel_shop_id" ON "Hotel" (shop_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE INDEX "IX_Hotel_sub_address_id" ON "Hotel" (sub_address_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE INDEX "IX_Pet_Service_Room_pet_type_id" ON "Pet_Service_Room" (pet_type_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE INDEX "IX_Pet_Service_Room_room_id" ON "Pet_Service_Room" (room_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE INDEX "IX_Pet_Service_Room_service_id" ON "Pet_Service_Room" (service_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE INDEX "IX_Room_Booking_customer_id" ON "Room_Booking" (customer_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    CREATE INDEX "IX_Room_Booking_room_detail_id" ON "Room_Booking" (room_detail_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    ALTER TABLE "Room" ADD CONSTRAINT "FK_Room_Hotel_Hotelid" FOREIGN KEY ("Hotelid") REFERENCES "Hotel" (id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250610021429_second') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250610021429_second', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Room" DROP CONSTRAINT "FK_Room_Hotel_Hotelid";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    DROP INDEX "IX_Room_Hotelid";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Room" DROP COLUMN "Hotelid";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Appointments" RENAME COLUMN end_type TO end_time;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Room_Booking" ADD created_at timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Room_Booking" ADD updated_at timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Room" ADD hotel_id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Pet_Service_Room" ADD created_at timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Pet_Service_Room" ADD updated_at timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Hotel" ADD created_at timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Hotel" ADD updated_at timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN

                    ALTER TABLE "Appointments" ADD COLUMN "status_temp" integer;
                    UPDATE "Appointments" SET "status_temp" = 
                        CASE 
                            WHEN "status" = true THEN 1
                            WHEN "status" = false THEN 0
                            ELSE 0
                        END;
                    ALTER TABLE "Appointments" DROP COLUMN "status";
                    ALTER TABLE "Appointments" RENAME COLUMN "status_temp" TO "status";
                    -- Set NOT NULL constraint and default value
                    ALTER TABLE "Appointments" ALTER COLUMN "status" SET NOT NULL;
                    ALTER TABLE "Appointments" ALTER COLUMN "status" SET DEFAULT 0;
                
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    UPDATE "Appointments" SET location_type = '' WHERE location_type IS NULL;
    ALTER TABLE "Appointments" ALTER COLUMN location_type SET NOT NULL;
    ALTER TABLE "Appointments" ALTER COLUMN location_type SET DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    CREATE INDEX "IX_Room_hotel_id" ON "Room" (hotel_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    ALTER TABLE "Room" ADD CONSTRAINT "FK_Room_Hotel_hotel_id" FOREIGN KEY (hotel_id) REFERENCES "Hotel" (id) ON DELETE CASCADE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250613150846_update') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250613150846_update', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617011227_payment') THEN
    ALTER TABLE "Account" ADD "BANK_ACCOUNT_NAME" text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617011227_payment') THEN
    ALTER TABLE "Account" ADD "BANK_ACCOUNT_NO" text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617011227_payment') THEN
    ALTER TABLE "Account" ADD "BANK_ID" text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617011227_payment') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250617011227_payment', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617125332_newdb') THEN
    ALTER TABLE "Service_Appointment" DROP CONSTRAINT "PK_Service_Appointment";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617125332_newdb') THEN
    ALTER TABLE "Service_Appointment" RENAME COLUMN end_type TO end_time;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617125332_newdb') THEN
    ALTER TABLE "Service_Appointment" ADD id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617125332_newdb') THEN
    ALTER TABLE "Service_Appointment" ADD CONSTRAINT "PK_Service_Appointment" PRIMARY KEY (id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617125332_newdb') THEN
    CREATE INDEX "IX_Service_Appointment_service_id" ON "Service_Appointment" (service_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250617125332_newdb') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250617125332_newdb', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    ALTER TABLE "Hotel" DROP CONSTRAINT "FK_Hotel_Sub_Address_sub_address_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    DROP INDEX "IX_Hotel_sub_address_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    UPDATE "Hotel" SET total_room = 0 WHERE total_room IS NULL;
    ALTER TABLE "Hotel" ALTER COLUMN total_room SET NOT NULL;
    ALTER TABLE "Hotel" ALTER COLUMN total_room SET DEFAULT 0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    UPDATE "Hotel" SET sub_address_id = '00000000-0000-0000-0000-000000000000' WHERE sub_address_id IS NULL;
    ALTER TABLE "Hotel" ALTER COLUMN sub_address_id SET NOT NULL;
    ALTER TABLE "Hotel" ALTER COLUMN sub_address_id SET DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    UPDATE "Hotel" SET name = '' WHERE name IS NULL;
    ALTER TABLE "Hotel" ALTER COLUMN name SET NOT NULL;
    ALTER TABLE "Hotel" ALTER COLUMN name SET DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    UPDATE "Hotel" SET available_room = 0 WHERE available_room IS NULL;
    ALTER TABLE "Hotel" ALTER COLUMN available_room SET NOT NULL;
    ALTER TABLE "Hotel" ALTER COLUMN available_room SET DEFAULT 0;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    CREATE UNIQUE INDEX "IX_Hotel_sub_address_id" ON "Hotel" (sub_address_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    ALTER TABLE "Hotel" ADD CONSTRAINT "FK_Hotel_Sub_Address_sub_address_id" FOREIGN KEY (sub_address_id) REFERENCES "Sub_Address" (id) ON DELETE CASCADE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250620112439_fix-hotel') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250620112439_fix-hotel', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623044746_changeField') THEN
    ALTER TABLE "Service_Appointment" DROP CONSTRAINT "FK_Service_Appointment_Room_room_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623044746_changeField') THEN
    DROP INDEX "IX_Service_Appointment_room_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623044746_changeField') THEN
    ALTER TABLE "Service_Appointment" DROP COLUMN end_time;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623044746_changeField') THEN
    ALTER TABLE "Service_Appointment" DROP COLUMN room_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623044746_changeField') THEN
    ALTER TABLE "Service_Appointment" DROP COLUMN start_time;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623044746_changeField') THEN
    ALTER TABLE "Appointments" DROP COLUMN end_time;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623044746_changeField') THEN
    ALTER TABLE "Appointments" DROP COLUMN location_type;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623044746_changeField') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250623044746_changeField', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623054015_changeField1') THEN
    ALTER TABLE "Service_Appointment" DROP CONSTRAINT "FK_Service_Appointment_Rating_rating_id";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623054015_changeField1') THEN
    ALTER TABLE "Service_Appointment" ALTER COLUMN rating_id DROP NOT NULL;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623054015_changeField1') THEN
    ALTER TABLE "Service_Appointment" ADD CONSTRAINT "FK_Service_Appointment_Rating_rating_id" FOREIGN KEY (rating_id) REFERENCES "Rating" (id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623054015_changeField1') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250623054015_changeField1', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623115334_cart') THEN
    CREATE TABLE "Cart" (
        id uuid NOT NULL,
        customer_id uuid NOT NULL,
        total double precision NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Cart" PRIMARY KEY (id),
        CONSTRAINT "FK_Cart_Customer_customer_id" FOREIGN KEY (customer_id) REFERENCES "Customer" (account_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623115334_cart') THEN
    CREATE TABLE "Service_Cart" (
        id uuid NOT NULL,
        cart_id uuid NOT NULL,
        service_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Service_Cart" PRIMARY KEY (id),
        CONSTRAINT "FK_Service_Cart_Cart_cart_id" FOREIGN KEY (cart_id) REFERENCES "Cart" (id) ON DELETE CASCADE,
        CONSTRAINT "FK_Service_Cart_Service_service_id" FOREIGN KEY (service_id) REFERENCES "Service" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623115334_cart') THEN
    CREATE UNIQUE INDEX "IX_Cart_customer_id" ON "Cart" (customer_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623115334_cart') THEN
    CREATE INDEX "IX_Service_Cart_cart_id" ON "Service_Cart" (cart_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623115334_cart') THEN
    CREATE INDEX "IX_Service_Cart_service_id" ON "Service_Cart" (service_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250623115334_cart') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250623115334_cart', '9.0.5');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250709083401_service-image') THEN
    ALTER TABLE "Shop" ADD "Phone" text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250709083401_service-image') THEN
    ALTER TABLE "Service" ADD img_url text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250709083401_service-image') THEN
    ALTER TABLE "Service" ADD img_url_id text;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250709083401_service-image') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250709083401_service-image', '9.0.5');
    END IF;
END $EF$;
COMMIT;


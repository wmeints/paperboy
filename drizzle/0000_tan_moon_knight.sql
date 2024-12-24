CREATE TABLE "categories" (
	"id" serial PRIMARY KEY NOT NULL,
	"name" varchar(100) NOT NULL,
	"description" text NOT NULL,
	"headerImage" varchar(1000),
	"dateCreated" timestamp DEFAULT now() NOT NULL,
	"dateUpdated" timestamp
);
--> statement-breakpoint
CREATE TABLE "posts" (
	"id" serial NOT NULL,
	"title" varchar(500) NOT NULL,
	"markdown" text,
	"html" text,
	"headerImage" varchar(1000),
	"excerpt" text NOT NULL,
	"dateCreated" timestamp DEFAULT now() NOT NULL,
	"dateUpdated" timestamp,
	"datePublished" timestamp
);

import {
    date,
    serial,
    pgTable as table,
    text,
    timestamp,
    varchar,
} from "drizzle-orm/pg-core";
import { sql } from "drizzle-orm";

export const postsTable = table("posts", {
    id: serial(),
    title: varchar({ length: 500 }).notNull(),
    markdown: text(),
    html: text(),
    headerImage: varchar({ length: 1000 }),
    excerpt: text().notNull(),
    dateCreated: timestamp().notNull().defaultNow(),
    dateUpdated: timestamp().$onUpdate(() => sql`now()`),
    datePublished: timestamp(),
});

import {
    serial,
    pgTable as table,
    text,
    timestamp,
    varchar,
} from "drizzle-orm/pg-core";
import { sql } from "drizzle-orm";
import { users } from "./users";

export const posts = table("posts", {
    id: serial(),
    authorId: text()
        .notNull()
        .references(() => users.id, { onDelete: "restrict" }),
    title: varchar({ length: 500 }).notNull(),
    markdown: text(),
    html: text(),
    headerImage: varchar({ length: 1000 }),
    excerpt: text().notNull(),
    dateCreated: timestamp().notNull().defaultNow(),
    dateUpdated: timestamp().$onUpdate(() => sql`now()`),
    datePublished: timestamp(),
});

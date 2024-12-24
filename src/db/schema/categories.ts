import {
    serial,
    pgTable as table,
    text,
    varchar,
    timestamp,
} from "drizzle-orm/pg-core";
import { sql } from "drizzle-orm";

export const categories = table("categories", {
    id: serial().primaryKey(),
    name: varchar({ length: 100 }).notNull(),
    description: text().notNull(),
    headerImage: varchar({ length: 1000 }),
    dateCreated: timestamp().notNull().defaultNow(),
    dateUpdated: timestamp().$onUpdate(() => sql`now()`),
});

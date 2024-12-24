import { config } from "dotenv";
import { defineConfig } from "drizzle-kit";

// Make sure to load the .env file and the .env.local file.
// The .env file is for production settings while the local version is for local development.
config({ path: ".env" });
config({ path: ".env.local" });

export default defineConfig({
    out: "./drizzle",
    schema: "./src/db/schema/",
    dialect: "postgresql",
    dbCredentials: {
        url: process.env.DATABASE_URL!,
    },
    verbose: true,
    strict: true,
});

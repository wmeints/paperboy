/**
 * Authentication module that configures NextAuth with JWT session strategy and Drizzle adapter.
 * For detailed authentication configuration, see the `auth.config` module.
 */

import NextAuth from "next-auth";
import { DrizzleAdapter } from "@auth/drizzle-adapter";
import { db } from "@/db/connection";
import authConfig from "./auth.config";

export const { signIn, signOut, handlers, auth } = NextAuth({
    session: { strategy: "jwt" },
    adapter: DrizzleAdapter(db),
    ...authConfig,
});

import NextAuth from "next-auth";
import { DrizzleAdapter } from "@auth/drizzle-adapter";
import { db } from "@/db/connection";
import authConfig from "./auth.config";

export const { signIn, signOut, handlers, auth } = NextAuth({
    session: { strategy: "jwt" },
    adapter: DrizzleAdapter(db),
    ...authConfig,
});

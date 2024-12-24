import NextAuth from "next-auth";
import Github from "next-auth/providers/github";
import { DrizzleAdapter } from "@auth/drizzle-adapter";
import { db } from "@/db/connection";

export const { signIn, signOut, handlers, auth } = NextAuth({
    providers: [Github],
    adapter: DrizzleAdapter(db),
    callbacks: {
        signIn({ profile }) {
            // Only allow my personal account to sign in on the website.
            // If you're using my code, make sure to change this ;-)
            if (profile?.login === "wmeints") {
                return true;
            }

            return false;
        },
        authorized({ request, auth }) {
            const { pathname } = request.nextUrl;

            // Authorize access to authenticated users for the editor page.
            if (!pathname.startsWith("/editor")) {
                return true;
            }

            return auth?.user != null;
        },
    },
});

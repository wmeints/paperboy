import Github from "next-auth/providers/github";
import { NextAuthConfig } from "next-auth";

export default {
    providers: [Github],
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
} satisfies NextAuthConfig;

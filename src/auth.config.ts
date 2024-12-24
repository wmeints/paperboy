/**
 * Configuration for the NextAuth authentication system.
 * This module is separated from the main auth module to work around limitations
 * with the edge runtime as described in the Auth.js v5 migration guide.
 * @see https://authjs.dev/getting-started/migrating-to-v5#edge-compatibility
 *
 * @remarks
 * The configuration includes:
 * - GitHub authentication provider setup
 * - Sign-in callback that restricts access to a specific GitHub user
 * - Authorization callback that protects the editor routes
 *
 * @returns {NextAuthConfig} The NextAuth configuration object
 */

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

import AppSidebar from "@/components/app-sidebar";
import { SidebarProvider } from "@/components/ui/sidebar";
import React from "react";

export default function EditorLayout({
    children,
}: {
    children: React.ReactNode;
}) {
    return (
        <SidebarProvider>
            <AppSidebar />
            <main className="flex flex-1 flex-col">{children}</main>
        </SidebarProvider>
    );
}

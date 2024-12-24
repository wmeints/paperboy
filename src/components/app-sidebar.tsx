import Link from "next/link";
import {
    Sidebar,
    SidebarContent,
    SidebarFooter,
    SidebarGroup,
    SidebarGroupContent,
    SidebarGroupLabel,
    SidebarHeader,
    SidebarMenu,
    SidebarMenuButton,
    SidebarMenuItem,
} from "./ui/sidebar";
import { Button } from "./ui/button";
import { Book, Bookmark, Globe, Plus, User2 } from "lucide-react";
import { DropdownMenu, DropdownMenuTrigger } from "./ui/dropdown-menu";
import { AvatarFallback } from "./ui/avatar";
import UserSidebarMenu from "./user-sidebar-menu";
import { SessionProvider } from "next-auth/react";

function ContentManagementSidebarGroup() {
    return (
        <SidebarGroup title="Manage content">
            <SidebarGroupLabel>Manage content</SidebarGroupLabel>
            <SidebarGroupContent>
                <SidebarMenu>
                    <SidebarMenuItem>
                        <SidebarMenuButton asChild>
                            <Link href="/editor/posts">
                                <Book />
                                Posts
                            </Link>
                        </SidebarMenuButton>
                    </SidebarMenuItem>
                    <SidebarMenuItem>
                        <SidebarMenuButton asChild>
                            <Link href="/editor/categories">
                                <Bookmark />
                                Categories
                            </Link>
                        </SidebarMenuButton>
                    </SidebarMenuItem>
                </SidebarMenu>
            </SidebarGroupContent>
        </SidebarGroup>
    );
}

function SiteSidebarGroup() {
    return (
        <SidebarGroup>
            <SidebarGroupLabel>Website</SidebarGroupLabel>
            <SidebarGroupContent>
                <SidebarMenu>
                    <SidebarMenuItem>
                        <SidebarMenuButton asChild>
                            <Link href="/" target="_blank">
                                <Globe />
                                Visit website
                            </Link>
                        </SidebarMenuButton>
                    </SidebarMenuItem>
                </SidebarMenu>
            </SidebarGroupContent>
        </SidebarGroup>
    );
}

export default function AppSidebar() {
    return (
        <SessionProvider>
            <Sidebar>
                <SidebarHeader>
                    <h1 className="font-semibold text-muted-foreground">
                        Paperboy
                    </h1>
                </SidebarHeader>
                <SidebarContent>
                    <SidebarGroup>
                        <Button variant="outline" asChild>
                            <Link href="/editor/posts/new">
                                <Plus />
                                New post
                            </Link>
                        </Button>
                    </SidebarGroup>
                    <ContentManagementSidebarGroup />
                    <SiteSidebarGroup />
                </SidebarContent>
                <SidebarFooter>
                    <UserSidebarMenu />
                </SidebarFooter>
            </Sidebar>
        </SessionProvider>
    );
}
